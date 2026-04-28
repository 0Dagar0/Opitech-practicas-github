import { createReducer, on } from '@ngrx/store';
import { AuthState, initialAuthState } from './auth.state';
import { AuthActions } from './auth.actions';

export const authReducer = createReducer(
    initialAuthState,

    on(AuthActions.login, (state) => ({
        ...state,
        loading: true,
        error: null,
    })),

    on(AuthActions.loginSuccess, (state, { username }) => ({
        ...state,
        isAuthenticated: true,
        user: { username },
        loading: false,
        error: null,
    })),

    on(AuthActions.loginFailure, (state, { error }) => ({
        ...state,
        isAuthenticated: false,
        user: null,
        loading: false,
        error,
    })),

    on(AuthActions.logout, (state) => ({
        ...state,
        loading: true,
    })),

    on(AuthActions.logoutSuccess, () => initialAuthState),

    on(AuthActions.checkAuth, (state) => state), // no cambia nada

    on(AuthActions.restoreAuth, (state, { username }) => ({
        ...state,
        isAuthenticated: true,
        user: { username },
        loading: false,
        error: null,
    }))
);