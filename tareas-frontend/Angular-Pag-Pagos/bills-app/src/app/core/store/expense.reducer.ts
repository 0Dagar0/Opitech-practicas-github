import { createReducer, on } from '@ngrx/store';
import { ExpenseState, initialState } from './expense.state';
import { ExpenseActions } from './expense.actions';
import { Expense } from '../models/expense.model';

const calculateTotal = (expenses: Expense[]): number => {
    return expenses.reduce((acc, curr) => acc + curr.amount, 0);
};

export const expenseReducer = createReducer(
    initialState,

    // Cargar gastos
    on(ExpenseActions.loadExpenses, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.loadExpensesSuccess, (state, { expenses }) => ({
        ...state,
        expenses: expenses,
        total: calculateTotal(expenses),
        loading: false,
        error: null
    })),

    on(ExpenseActions.loadExpensesFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error: error
    })),

    // Agregar gasto
    on(ExpenseActions.addExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.addExpenseSuccess, (state, { expense }) => ({
        ...state,
        expenses: [...state.expenses, expense],
        total: calculateTotal([...state.expenses, expense]),
        loading: false
    })),

    on(ExpenseActions.addExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error: error
    })),

    // Actualizar gasto
    on(ExpenseActions.updateExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.updateExpenseSuccess, (state, { expense }) => ({
        ...state,
        expenses: state.expenses.map(g => g.id === expense.id ? expense : g),
        total: calculateTotal(state.expenses.map(g => g.id === expense.id ? expense : g)),
        loading: false
    })),

    on(ExpenseActions.updateExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error: error
    })),

    // Eliminar gasto
    on(ExpenseActions.deleteExpense, (state) => ({
        ...state,
        loading: true,
        error: null
    })),

    on(ExpenseActions.deleteExpenseSuccess, (state, { id }) => ({
        ...state,
        expenses: state.expenses.filter(g => g.id !== id),
        total: calculateTotal(state.expenses.filter(g => g.id !== id)),
        loading: false
    })),

    on(ExpenseActions.deleteExpenseFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error: error
    }))
);