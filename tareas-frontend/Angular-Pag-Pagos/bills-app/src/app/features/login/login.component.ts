import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { AuthActions } from '../../core/store/auth/auth.actions';
import { Observable } from 'rxjs';
import { AuthState } from '../../core/store/auth/auth.state';

// Validador personalizado: al menos una mayúscula y una minúscula
function passwordCaseValidator(control: any) {
    const value = control.value || '';
    const hasLower = /[a-z]/.test(value);
    const hasUpper = /[A-Z]/.test(value);
    if (!hasLower || !hasUpper) {
        return { case: true };
    }
    return null;
}

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule], // 👈 Cambiamos FormsModule por ReactiveFormsModule
    template: `
    <div class="login-container">
    <div class="login-card">
        <h2>Iniciar Sesión</h2>
        <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
        <!-- Campo Usuario -->
        <div class="form-group">
            <label>Usuario</label>
            <input type="text" formControlName="username" placeholder="Ingrese su usuario">
            <div *ngIf="username?.invalid && username?.touched" class="error-message">
              <div *ngIf="username?.errors?.['required']">⚠️ El usuario es obligatorio</div>
              <div *ngIf="username?.errors?.['minlength']">📝 Mínimo 3 caracteres</div>
            </div>
        </div>

        <!-- Campo Contraseña -->
        <div class="form-group">
            <label>Contraseña</label>
            <input type="password" formControlName="password" placeholder="Ingrese su contraseña">
            <div *ngIf="password?.invalid && password?.touched" class="error-message">
              <div *ngIf="password?.errors?.['required']">⚠️ La contraseña es obligatoria</div>
              <div *ngIf="password?.errors?.['minlength']">📝 Mínimo 4 caracteres</div>
              <div *ngIf="password?.errors?.['case']">🔤 Debe contener al menos una mayúscula y una minúscula</div>
            </div>
        </div>

          <div *ngIf="error$ | async as error" class="error">
            {{ error }}
        </div>

        <button type="submit" [disabled]="(loading$ | async) || loginForm.invalid">
            {{ (loading$ | async) ? 'Cargando...' : 'Ingresar' }}
        </button>
        </form>
    </div>
    </div>`,

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
    input.ng-invalid.ng-touched {
        border-color: #ef4444;
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
        cursor: not-allowed;
    }
    .error {
        color: #ef4444;
        margin-bottom: 1rem;
        font-size: 0.875rem;
    }
    .error-message {
        color: #ef4444;
        font-size: 0.75rem;
        margin-top: 0.25rem;
    }
`]
})
export class LoginComponent implements OnInit {
    private store = inject(Store<{ auth: AuthState }>);
    private router = inject(Router);
    private fb = inject(FormBuilder);

    loginForm: FormGroup;
    loading$: Observable<boolean>;
    error$: Observable<string | null>;

    constructor() {
        // Crear formulario reactivo con validaciones
        this.loginForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(3)]],
            password: ['', [Validators.required, Validators.minLength(4), passwordCaseValidator]]
        });

        this.loading$ = this.store.select(state => state.auth.loading);
        this.error$ = this.store.select(state => state.auth.error);
    }

    ngOnInit() {
        this.store.select(state => state.auth.isAuthenticated).subscribe(isAuthenticated => {
            if (isAuthenticated) {
                this.router.navigate(['/dashboard']);
            }
        });
    }

    // Getters para facilitar el acceso en el template
    get username() { return this.loginForm.get('username'); }
    get password() { return this.loginForm.get('password'); }

    onSubmit() {
        if (this.loginForm.invalid) {
            this.loginForm.markAllAsTouched();
            return;
        }

        const { username, password } = this.loginForm.value;
        this.store.dispatch(AuthActions.login({ username, password }));
    }
}