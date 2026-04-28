import { createActionGroup, emptyProps, props } from '@ngrx/store';

export const AuthActions = createActionGroup({
    source: 'Auth',
    events: {
        'Login': props<{ username: string; password: string }>(),
        'Login Success': props<{ username: string }>(),
        'Login Failure': props<{ error: string }>(),
        'Logout': emptyProps(),
        'Logout Success': emptyProps(),
        'Check Auth': emptyProps(),
        'Restore Auth': props<{ username: string }>(),
    }
});