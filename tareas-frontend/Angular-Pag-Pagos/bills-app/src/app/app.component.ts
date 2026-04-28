import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthActions } from './core/store/auth/auth.actions';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet], 
    template: `<router-outlet></router-outlet>`,
})
export class App implements OnInit {
    private store = inject(Store);
    ngOnInit() {
        this.store.dispatch(AuthActions.checkAuth());
    }
}