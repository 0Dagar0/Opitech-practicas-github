/// <reference types="vitest" />
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { LoginComponent } from './login.component';
import { AuthActions } from '../../core/store/auth/auth.actions';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;

    const mockStore = {
        dispatch: vi.fn(),
        select: vi.fn().mockReturnValue(of(false))
    };

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [LoginComponent],
            providers: [
                provideRouter([]),
                {
                    provide: Store,
                    useValue: mockStore
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should have invalid form when empty', () => {
        expect(component.loginForm.invalid).toBeTruthy();
    });

    it('should dispatch login action', () => {
        component.loginForm.setValue({
            username: 'William',
            password: 'Test123'
        });

        component.onSubmit();

        expect(mockStore.dispatch).toHaveBeenCalledWith(
            AuthActions.login({
                username: 'William',
                password: 'Test123'
            })
        );
    });
});