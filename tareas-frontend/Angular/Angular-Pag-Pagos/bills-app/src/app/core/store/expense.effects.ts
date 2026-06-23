import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { HttpClient } from '@angular/common/http';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { ExpenseActions } from './expense.actions';
import { Expense } from '../models/expense.model';

@Injectable()
export class ExpenseEffects {
    private actions$ = inject(Actions);
    private http = inject(HttpClient);
    private apiUrl = 'https://localhost:7072/expenses';

    // 1. Cargar gastos
    loadExpenses$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ExpenseActions.loadExpenses),
            mergeMap(() =>
                this.http.get<Expense[]>(this.apiUrl).pipe(
                    map((expenses) => ExpenseActions.loadExpensesSuccess({ expenses })),
                    catchError((error) =>
                        of(ExpenseActions.loadExpensesFailure({ error: error.message }))
                    )
                )
            )
        )
    );

    // 2. Agregar gasto
    addExpense$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ExpenseActions.addExpense),
            mergeMap(({ expense }) => {
                const expenseToSend = {
                    ...expense,
                    category: this.getCategoryNumber(expense.category)
                };
                return this.http.post<Expense>(`${this.apiUrl}/add`, expenseToSend).pipe(
                    map((newExpense) => ExpenseActions.addExpenseSuccess({ expense: newExpense })),
                    catchError((error) =>
                        of(ExpenseActions.addExpenseFailure({ error: error.message }))
                    )
                );
            })
        )
    );

    // 3. Actualizar gasto
    updateExpense$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ExpenseActions.updateExpense),
            mergeMap(({ expense }) => {
                const expenseToSend = {
                    ...expense,
                    category: this.getCategoryNumber(expense.category)
                };
                return this.http.put(`${this.apiUrl}/update/${expense.id}`, expenseToSend).pipe(
                    map(() => ExpenseActions.updateExpenseSuccess({ expense })),
                    catchError((error) =>
                        of(ExpenseActions.updateExpenseFailure({ error: error.message }))
                    )
                );
            })
        )
    );

    // 4. Eliminar gasto
    deleteExpense$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ExpenseActions.deleteExpense),
            mergeMap(({ id }) =>
                this.http.delete(`${this.apiUrl}/delete/${id}`).pipe(
                    map(() => ExpenseActions.deleteExpenseSuccess({ id })),
                    catchError((error) =>
                        of(ExpenseActions.deleteExpenseFailure({ error: error.message }))
                    )
                )
            )
        )
    );

    // Método auxiliar para convertir categoría
    private getCategoryNumber(category: string | number): number {
        const categoriesMap: { [key: string]: number } = {
            'Food': 0,
            'Transportation': 1,
            'Entertainment': 2,
            'Utilities': 3,
            'Healthcare': 4,
            'Other': 5
        };

        if (typeof category === 'number') return category;
        return categoriesMap[category] ?? 5;
    }
}