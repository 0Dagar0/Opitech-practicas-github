import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Expense } from '../../core/models/expense.model';
import { ExpenseActions } from '../../core/store/expense.actions';
import { ExpenseState } from '../../core/store/expense.state';
import { ModalComponent } from '../../shared/components/modal/modal.component';

@Component({
    selector: 'app-expenses',
    standalone: true,
    imports: [CommonModule, FormsModule, ModalComponent],
    templateUrl: './expenses.component.html',
    styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
    private store = inject(Store<{ expenses: ExpenseState }>);

    isModalOpen = false;
    modalTitle = '';
    isEditModalOpen = false;
    editModalTitle = '✏️ Editar Gasto';
    isDeleteModalOpen = false;
    deleteModalTitle = '🗑️ Confirmar Eliminación';
    deleteExpenseId: number | null = null;

    expenses$: Observable<Expense[]>;
    total$: Observable<number>;
    loading$: Observable<boolean>;
    error$: Observable<string | null>;

    newExpense: Omit<Expense, 'id'> = {
        description: '',
        amount: 0,
        date: new Date(),
        category: 0
    };

    editingExpense: Expense | null = null;

    isAddFormDirty = false;
    isEditFormDirty = false;

    constructor() {
        this.expenses$ = this.store.select(state => state.expenses.expenses);
        this.total$ = this.store.select(state => state.expenses.total);
        this.loading$ = this.store.select(state => state.expenses.loading);
        this.error$ = this.store.select(state => state.expenses.error);
    }

    ngOnInit(): void {
        this.store.dispatch(ExpenseActions.loadExpenses());
    }

    addExpense(): void {
        if (!this.newExpense.description || this.newExpense.amount <= 0) {
            alert('Por favor complete los campos');
            return;
        }
        const expenseToSend = {
            ...this.newExpense,
            date: new Date(this.newExpense.date)
        };
        this.store.dispatch(ExpenseActions.addExpense({ expense: expenseToSend }));
    }

    updateExpense(): void {
        if (this.editingExpense) {
            this.store.dispatch(ExpenseActions.updateExpense({ expense: this.editingExpense }));
        }
    }

    getCategoryName(category: number): string {
        const categories = ['Food', 'Transportation', 'Entertainment', 'Utilities', 'Healthcare', 'Other'];
        return categories[category] || 'Other';
    }

    // ========== MODAL AGREGAR ==========
    openAddModal(): void {
        this.isAddFormDirty = false;
        this.modalTitle = '➕ Agregar Nuevo Gasto';
        this.isModalOpen = true;
    }

    closeModal(): void {
        if (this.isAddFormDirty) {
            const confirm = window.confirm('Hay cambios sin guardar. ¿Estás seguro de que quieres salir?');
            if (!confirm) return;
        }
        this.isModalOpen = false;
        this.isAddFormDirty = false;
        this.newExpense = {
            description: '',
            amount: 0,
            date: new Date(),
            category: 0
        };
    }

    confirmAdd(): void {
        if (this.isAddFormDirty) {
            this.isAddFormDirty = false;
        }
        this.addExpense();
        this.closeModal();
    }

    markAddFormDirty(): void {
        this.isAddFormDirty = true;
    }

    // ========== MODAL EDITAR ==========
    openEditModal(expense: Expense): void {
        this.isEditFormDirty = false;
        this.editingExpense = { ...expense };
        this.isEditModalOpen = true;
    }

    closeEditModal(): void {
        if (this.isEditFormDirty) {
            const confirm = window.confirm('Hay cambios sin guardar. ¿Estás seguro de que quieres salir?');
            if (!confirm) return;
        }
        this.isEditModalOpen = false;
        this.isEditFormDirty = false;
        this.editingExpense = null;
    }

    confirmEdit(): void {
        if (this.isEditFormDirty) {
            this.isEditFormDirty = false;
        }
        this.updateExpense();
        this.closeEditModal();
    }

    markEditFormDirty(): void {
        this.isEditFormDirty = true;
    }

    // ========== MODAL ELIMINAR ==========
    openDeleteModal(id: number): void {
        this.deleteExpenseId = id;
        this.isDeleteModalOpen = true;
    }

    closeDeleteModal(): void {
        this.isDeleteModalOpen = false;
        this.deleteExpenseId = null;
    }

    confirmDelete(): void {
        if (this.deleteExpenseId !== null) {
            this.store.dispatch(ExpenseActions.deleteExpense({ id: this.deleteExpenseId }));
        }
        this.closeDeleteModal();
    }
}