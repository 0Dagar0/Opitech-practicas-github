import { createReducer, on } from '@ngrx/store';
import { Expense } from '../models/expense.model';
import { ExpenseActions } from './expense.actions';

export interface ExpenseState {
    expenses: Expense[];
    total: number;
    loading: boolean;
    error: string | null;
}

export const initialState: ExpenseState = {
    expenses: [],
    total: 0,
    loading: false,
    error: null
};

export const expenseReducer = createReducer(
    initialState,

    on(ExpenseActions.loadExpenses, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.loadExpensesSuccess, (state, { expenses }) => ({
        ...state,
        expenses,
        loading: false,
        total: expenses.reduce((sum, exp) => sum + exp.amount, 0)
    })),

    on(ExpenseActions.loadExpensesFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error
    })),

    on(ExpenseActions.addExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.addExpenseSuccess, (state, { expense }) => ({
        ...state,
        expenses: [...state.expenses, expense],
        loading: false,
        total: state.total + expense.amount
    })),

    on(ExpenseActions.addExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error
    })),

    on(ExpenseActions.updateExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.updateExpenseSuccess, (state, { expense }) => {
        const updatedExpenses = state.expenses.map(e =>
            e.id === expense.id ? expense : e
        );
        const newTotal = updatedExpenses.reduce((sum, e) => sum + e.amount, 0);

        return {
            ...state,
            expenses: updatedExpenses,
            loading: false,
            total: newTotal
        };
    }),

    on(ExpenseActions.updateExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error
    })),

    on(ExpenseActions.deleteExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.deleteExpenseSuccess, (state, { id }) => {
        const updatedExpenses = state.expenses.filter(e => e.id !== id);
        const newTotal = updatedExpenses.reduce((sum, e) => sum + e.amount, 0);

        return {
            ...state,
            expenses: updatedExpenses,
            loading: false,
            total: newTotal
        };
    }),

    on(ExpenseActions.deleteExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error
    }))
);

// selectors (para usar en el componente)
// SELECTORS CORREGIDOS - Reciben el estado global de la app
export const selectExpenses = (state: any) => state.expenses.expenses;
export const selectExpensesTotal = (state: any) => state.expenses.total;
export const selectExpensesLoading = (state: any) => state.expenses.loading;
export const selectExpensesError = (state: any) => state.expenses.error;