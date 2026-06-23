import { vi } from 'vitest';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { of } from 'rxjs';
import { Store } from '@ngrx/store';

import { ExpensesComponent } from './expenses.component';

describe('ExpensesComponent', () => {
    let component: ExpensesComponent;
    let fixture: ComponentFixture<ExpensesComponent>;

    const mockStore = {
        dispatch: vi.fn(),
        select: vi.fn((selector) => {

            if (selector.name === 'selectExpenses') {
                return of([]);
            }

            if (selector.name === 'selectExpensesTotal') {
                return of(0);
            }

            if (selector.name === 'selectExpensesLoading') {
                return of(false);
            }

            if (selector.name === 'selectExpensesError') {
                return of(null);
            }

            return of(null);
        })
    };

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ExpensesComponent],
            providers: [
                provideRouter([]),
                {
                    provide: Store,
                    useValue: mockStore
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(ExpensesComponent);
        component = fixture.componentInstance;

        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should dispatch loadExpenses on init', () => {
        expect(mockStore.dispatch).toHaveBeenCalled();
    });
});
