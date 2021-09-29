import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        data: {
            customBreadcrumb: 'Trang chủ'
        },
        children: [
            {
                path: '',
                redirectTo: 'dashboard',
                pathMatch: 'prefix',
            },
            {
                path: 'dashboard',
                loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                data: {
                    customBreadcrumb: 'Bảng tin'
                }
            },
            {
                path: 'systems',
                loadChildren: () => import('./systems/systems.module').then((m) => m.SystemsModule),
                data: {
                    customBreadcrumb: 'Hệ thống'
                }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
