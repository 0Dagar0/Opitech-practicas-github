import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { ExpensesComponent } from './features/expenses/expenses.component';
import { Layout } from './layout/layout/layout.component';


@Component({
        template: '<h2>Bienvenido al Dashboard</h2><p>Selecciona una opción en el menú.</p>',
        standalone: true
}) export class DashboardPlaceholderComponent { }

@Component({
        template: '<h2>Sobre Nosotros</h2><p>Información sobre la aplicación.</p>',
        standalone: true
}) export class AboutPlaceholderComponent { }

@Component({
        template: '<h2>Contacto</h2><p>Página de contacto.</p>',
        standalone: true
}) export class ContactPlaceholderComponent { }

export const routes: Routes = [
        {
                path: '',
                component: Layout,
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
