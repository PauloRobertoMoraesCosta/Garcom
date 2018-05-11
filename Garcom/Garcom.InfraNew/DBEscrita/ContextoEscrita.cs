using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Dominio.Entidade.Models.Excecao;
using Garcom.Infra.DBEscrita.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;

namespace Garcom.Infra.DBEscrita
{
    public class ContextoEscrita : DbContext
    {
        private bool _rolledback;
        public DbContextTransaction Transaction { get; private set; }

        public ContextoEscrita()
             : base("dbgarcom")
        {}

        public DbContextTransaction StartTransaction() => Transaction ?? (Transaction = Database.BeginTransaction());

        public void Commit()
        {

            if (Transaction != null && !_rolledback)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public void Rollback()
        {
            if (Transaction?.UnderlyingTransaction.Connection != null && !_rolledback)
            {
                Transaction.Rollback();
                _rolledback = true;
            }
        }

        public void Save()
        {
            try
            {
                ChangeTracker.DetectChanges();
                SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                Rollback();
                throw new Exception(string.Join(Environment.NewLine,
                                    exception.EntityValidationErrors
                                        .SelectMany(e => e.ValidationErrors)
                                        .Select(e => $"{e.PropertyName} => {e.ErrorMessage}")));
            }
            catch (Exception ex)
            {
                Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void SaveAndCommit()
        {
            Save();
            Commit();
        }

        /// <summary>
        /// Tabela Perfil
        /// </summary>
        public DbSet<Perfil> Perfies { get; set; }
        /// <summary>
        /// Tabela Usuario
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }
        /// <summary>
        /// Tabela Ingrediente
        /// </summary>
        public DbSet<Ingrediente> Ingredientes { get; set; }
        /// <summary>
        /// Tabela Produtos
        /// </summary>
        public DbSet<Produto> Produtos { get; set; }
        /// <summary>
        /// Tabela Produto_Ingrediente
        /// </summary>
        public DbSet<ProdutoIngrediente> ProdutoIngredientes { get; set; }
        /// <summary>
        /// Tabela Auditoria
        /// </summary>
        public DbSet<Auditoria> Auditorias { get; set; }
        /// <summary>
        /// Tabela tamanho_produto
        /// </summary>
        public DbSet<TamanhoProduto> TamanhosProdutos { get; set; }
        /// <summary>
        /// Tabela grupo_produto
        /// </summary>
        public DbSet<GrupoProduto> GruposProdutos { get; set; }
        /// <summary>
        /// Tabela produto_ingrediente_tamanho_produto
        /// </summary>
        public DbSet<ProdutoIngredienteTamanhoValor> ProdutosIngredientesTamanhosValores { get; set; }
        /// <summary>
        /// Tabela Mesas
        /// </summary>
        public DbSet<Mesa> Mesas { get; set; }
        /// <summary>
        /// Tabela Excecao
        /// </summary>
        public DbSet<Excecao> Excecoes { get; set; }
        /// <summary>
        /// Tabela grupo_produto_tamanho_produto
        /// </summary>
        public DbSet<GrupoProdutoTamanhoProduto> GruposProdutoTamanhosProduto { get; set; }
        /// <summary>
        /// Tabela grupo_produto_tamanho_produto_valor
        /// </summary>
        public DbSet<ProdutoTamanhoValor> ProdutosTamanhosValores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            modelBuilder.Configurations.Add(new AuditoriaConfiguracao());
            modelBuilder.Configurations.Add(new PerfilConfiguracao());
            modelBuilder.Configurations.Add(new UsuarioConfiguracao());
            modelBuilder.Configurations.Add(new IngredienteConfiguracao());
            modelBuilder.Configurations.Add(new ProdutoConfiguracao());
            modelBuilder.Configurations.Add(new ProdutoIngredienteConfiguracao());
            modelBuilder.Configurations.Add(new TamanhoProdutoConfiguracao());
            modelBuilder.Configurations.Add(new GrupoProdutoConfiguracao());
            modelBuilder.Configurations.Add(new ProdutoIngredienteTamanhoValorConfiguracao());
            modelBuilder.Configurations.Add(new MesaConfiguracao());
            modelBuilder.Configurations.Add(new ExcecaoConfiguracao());
            modelBuilder.Configurations.Add(new GrupoProdutoTamanhoProdutoConfiguracao());
            modelBuilder.Configurations.Add(new ProdutoTamanhoValorConfiguracao());
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        /// <summary>
        /// Método responsável por retornar os atributos que sofreram as alterações no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChangeLog> RetornaModificacoes()
        {
            var modifiedEntities = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList();

            var ChangeLogs = new List<ChangeLog>();
            var dataAtividade = DateTime.Now;

            foreach (var change in modifiedEntities)
            {
                var changeLog = new ChangeLog();
                var dadosOriginais = change.GetDatabaseValues();
                changeLog.Entity = change.Entity.GetType().Name;
                changeLog.PrimaryKeyValue = GetPrimaryKeyValue(change).ToString();
                var operacao = change.State;

                switch(operacao)
                {
                    case EntityState.Deleted:
                        changeLog.State = Acao.EXCLUSAO;
                        break;
                    case EntityState.Modified:
                        changeLog.State = Acao.ALTERACAO;
                        break;
                }

                if (changeLog.State == Acao.EXCLUSAO)
                {
                    ChangeLogs.Add(changeLog);
                    continue;
                }

                if (dadosOriginais == null)
                    continue;
                
                bool alterou = false;
                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = Convert.ToString(dadosOriginais.GetValue<dynamic>(prop) ?? "");
                    var currentValue = Convert.ToString(change.CurrentValues[prop] ?? "");

                    if ((originalValue != currentValue) && prop != "DataCadastro")
                    {
                        alterou = true;
                        changeLog.PropertiesChangeLog.Add(new PropertyChangeLog
                        {
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = dataAtividade
                        });
                    }
                }

                if (alterou)
                    ChangeLogs.Add(changeLog);
            }

            return ChangeLogs;
        }
    }
}
