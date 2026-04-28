import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { AuthActions } from '../../core/store/auth/auth.actions';
import { Observable } from 'rxjs';
import { AuthState } from '../../core/store/auth/auth.state';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, FormsModule],
    template: `
    <div class="login-container">
    <div class="login-card">
        <h2>Iniciar Sesión</h2>
        <form (ngSubmit)="onSubmit()">
        <div class="form-group">
            <label>Usuario</label>
            <input type="text" [(ngModel)]="username" name="username" required>
        </div>
        <div class="form-group">
            <label>Contraseña</label>
            <input type="password" [(ngModel)]="password" name="password" required>
        </div>
        <div *ngIf="error$ | async as error" class="error">
            {{ error }}
        </div>
        <button type="submit" [disabled]="loading$ | async">
            {{ (loading$ | async) ? 'Cargando...' : 'Ingresar' }}
        </button>
        </form>
    </div>
    </div>
    `,
    styles: [`
    .login-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background: #f1f5f9;
    }
    .login-card {
        background: white;
        padding: 2rem;
        border-radius: 12px;
        box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
        width: 100%;
        max-width: 400px;
    }
    .form-group {
        margin-bottom: 1rem;
    }
    label {
        display: block;
        margin-bottom: 0.5rem;
        font-weight: 600;
    }
    input {
        width: 100%;
        padding: 0.5rem;
        border: 1px solid #cbd5e1;
        border-radius: 8px;
    }
    button {
        width: 100%;
        padding: 0.5rem;
        background: #3b82f6;
        color: white;
        border: none;
        border-radius: 8px;
        cursor: pointer;
    }
    button:disabled {
        background: #94a3b8;
    }
    .error {
        color: #ef4444;
        margin-bottom: 1rem;
    }
    `]
})
export class LoginComponent implements OnInit {
    private store = inject(Store<{ auth: AuthState }>);
    private router = inject(Router);
    username = '';
    password = '';
    loading$ = this.store.select(state => state.auth.loading);
    error$ = this.store.select(state => state.auth.error);

    ngOnInit() {
        this.store.select(state => state.auth.isAuthenticated).subscribe(isAuthenticated => {
            if (isAuthenticated) {
                this.router.navigate(['/dashboard']);
            }
        });
    }

    onSubmit() {
        if (!this.username.trim() || !this.password.trim()) {
            alert('Usuario y contraseña son obligatorios');
            return;
        }
        if (this.password.length < 4) {
            alert('La contraseña debe tener al menos 4 caracteres');
            return;
        }
        this.store.dispatch(AuthActions.login({ username: this.username, password: this.password }));
    }
}