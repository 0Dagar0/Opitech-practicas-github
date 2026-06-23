import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Store } from '@ngrx/store';
import { vi } from 'vitest';
import { Sidebar } from './sidebar';
import { AuthActions } from '../../core/store/auth/auth.actions';
import { provideRouter, Router } from '@angular/router';

describe('Sidebar', () => {
  let component: Sidebar;
  let fixture: ComponentFixture<Sidebar>;

  const mockStore = {
    dispatch: vi.fn()
  };

  const mockRouter = {
    navigate: vi.fn()
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Sidebar],
      providers: [
        provideRouter([
          {
            path: 'login',
            component: Sidebar
          }
        ]),
        {
          provide: Store,
          useValue: mockStore
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Sidebar);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should dispatch logout action', () => {
    component.logout();

    expect(mockStore.dispatch).toHaveBeenCalledWith(
      AuthActions.logout()
    );
  });

  it('should navigate to login', () => {
    component.logout();

    const router = TestBed.inject(Router);
    const navigateSpy = vi.spyOn(router, 'navigate');

    component.logout();

    expect(navigateSpy).toHaveBeenCalledWith(['/login']);
  });
});