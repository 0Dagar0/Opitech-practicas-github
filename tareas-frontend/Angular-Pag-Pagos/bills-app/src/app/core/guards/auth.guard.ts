import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthState } from '../store/auth/auth.state';
import { map, take } from 'rxjs/operators';

export const AuthGuard: CanActivateFn = () => {
    const store = inject(Store<{ auth: AuthState }>);
    const router = inject(Router);

    return store.select(state => state.auth.isAuthenticated).pipe(
        take(1),
        map(isAuthenticated => {
            if (isAuthenticated) {
                return true;
            } else {
                router.navigate(['/login']);
                return false;
            }
        })
    );
};