import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Spinner } from './components/spinner/spinner.component';
import { CheckBox } from './components/check-box/check-box.component';
import { AuthGuard } from './guards/auth.guard';
import { Confirm, ConfirmComponent } from './services/confirm/confirm.service';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { FilterPipe } from './pipes/filter.pipe';
import { OrderByPipe } from './pipes/orderby.pipe';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        Spinner,
        CheckBox,
        BreadcrumbComponent,
        ConfirmComponent,
        FilterPipe,
        OrderByPipe
    ],
    providers: [
        FilterPipe,
        OrderByPipe
    ],
    exports:[
        Spinner,
        CheckBox,
        BreadcrumbComponent,
        ConfirmComponent,
        FilterPipe,
        OrderByPipe
    ],
    entryComponents: [ConfirmComponent]
})
export class SharedModule { }
