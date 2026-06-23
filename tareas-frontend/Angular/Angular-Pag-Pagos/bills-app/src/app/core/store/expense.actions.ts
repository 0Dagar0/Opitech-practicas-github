import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Expense } from "../models/expense.model";

export const ExpenseActions = createActionGroup({
    source: 'Expense',
    events: {
        'Load Expenses': emptyProps(),
        'Load Expenses Success': props<{ expenses: Expense[] }>(),
        'Load Expenses Failure': props<{ error: string }>(),

        'Add Expense': props<{ expense: Omit<Expense, 'id'> }>(),
        'Add Expense Success': props<{ expense: Expense }>(),
        'Add Expense Failure': props<{ error: string }>(),

        'Update Expense': props<{ expense: Expense }>(),
        'Update Expense Success': props<{ expense: Expense }>(),
        'Update Expense Failure': props<{ error: string }>(),

        'Delete Expense': props<{ id: number }>(),
        'Delete Expense Success': props<{ id: number }>(),
        'Delete Expense Failure': props<{ error: string }>(),
    }
});