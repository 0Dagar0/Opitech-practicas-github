import { TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { Store } from '@ngrx/store';
import { vi } from 'vitest';
import { App } from './app';
import { AuthActions } from './core/store/auth/auth.actions';

describe('App', () => {
  const mockStore = {
    dispatch: vi.fn()
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [
        provideRouter([]),
        {
          provide: Store,
          useValue: mockStore
        }
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;

    expect(app).toBeTruthy();
  });

  it('should dispatch checkAuth on init', () => {
    const fixture = TestBed.createComponent(App);

    fixture.detectChanges();

    expect(mockStore.dispatch).toHaveBeenCalledWith(
      AuthActions.checkAuth()
    );
  });
});