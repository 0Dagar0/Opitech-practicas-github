import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { ExpensesComponent } from './features/expenses/expenses.component';
import { Layout } from './layout/layout/layout.component';
import { LoginComponent } from './features/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';

@Component({
        selector: 'app-dashboard-placeholder',
        host: { 'data-id': 'dashboard' },
        template: '<h2>Bienvenido al Dashboard</h2><p>Selecciona una opción en el menú.</p>',
        standalone: true
})
export class DashboardPlaceholderComponent { }

@Component({
        selector: 'app-about-placeholder',
        host: { 'data-id': 'about' },
        template: '<h2>Sobre Nosotros</h2><p>Información sobre la aplicación.</p>',
        standalone: true
})
export class AboutPlaceholderComponent { }

@Component({
        selector: 'app-contact-placeholder',
        host: { 'data-id': 'contact' },
        template: '<h2>Contacto</h2><p>Página de contacto.</p>',
        standalone: true
})
export class ContactPlaceholderComponent { }

export const routes: Routes = [
        { path: 'login', component: LoginComponent },
        {
                path: '',
                component: Layout,
                canActivate: [AuthGuard],
                children: [
                        { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                        { path: 'dashboard', component: DashboardPlaceholderComponent },
                        { path: 'about', component: AboutPlaceholderComponent },
                        { path: 'contact', component: ContactPlaceholderComponent },
                        { path: 'expenses', component: ExpensesComponent },
                ]
        },
        { path: '**', redirectTo: '' }
];