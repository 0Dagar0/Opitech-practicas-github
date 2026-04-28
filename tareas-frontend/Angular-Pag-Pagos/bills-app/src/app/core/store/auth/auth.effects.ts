import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, mergeMap, catchError, delay } from 'rxjs/operators';
import { AuthActions } from './auth.actions';

@Injectable()
export class AuthEffects {
    private actions$ = inject(Actions);

    login$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AuthActions.login),
            mergeMap(({ username, password }) => {
                const isValid = username === 'admin' && password === '1234';
                if (isValid) {
                    localStorage.setItem('auth', JSON.stringify({ isAuthenticated: true, username }));
                    return of(AuthActions.loginSuccess({ username })).pipe(delay(500));
                } else {
                    return of(AuthActions.loginFailure({ error: 'Usuario o contraseña incorrectos' })).pipe(delay(500));
                }
            })
        )
    );

    logout$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AuthActions.logout),
            map(() => {
                localStorage.removeItem('auth');
                return AuthActions.logoutSuccess();
            })
        )
    );

    checkAuth$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AuthActions.checkAuth),
            map(() => {
                const stored = localStorage.getItem('auth');
                if (stored) {
                    const data = JSON.parse(stored);
                    if (data.isAuthenticated && data.username) {
                        return AuthActions.restoreAuth({ username: data.username });
                    }
                }
                return AuthActions.logoutSuccess(); // si no hay sesión, logout implícito
            })
        )
    );

}