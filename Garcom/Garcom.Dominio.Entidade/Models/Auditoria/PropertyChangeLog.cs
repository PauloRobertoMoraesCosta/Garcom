using System;

namespace Garcom.Dominio.Entidade.Models.Auditoria
{
    public class PropertyChangeLog
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
