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

    isModalOpen: boolean = false;
    modalTitle: string = '';
    isEditModalOpen: boolean = false;
    editModalTitle: string = '✏️ Editar Gasto';
    isDeleteModalOpen: boolean = false;
    deleteModalTitle: string = '🗑️ Confirmar Eliminación';
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

        this.store.dispatch(ExpenseActions.addExpense({
            expense: expenseToSend
        }));

    }

    editExpense(expense: Expense): void {
        this.editingExpense = { ...expense };
    }

    updateExpense(): void {
        if (this.editingExpense) {
            this.store.dispatch(ExpenseActions.updateExpense({
                expense: this.editingExpense
            }));
        }
    }

    cancelEdit(): void {
        this.editingExpense = null;
    }

    deleteExpense(id: number): void {
        if (confirm('¿Eliminar este gasto?')) {
            this.store.dispatch(ExpenseActions.deleteExpense({ id }));
        }
    }

    getCategoryName(category: number): string {
        const categories = ['Food', 'Transportation', 'Entertainment', 'Utilities', 'Healthcare', 'Other'];
        return categories[category] || 'Other';
    }

    formatDateForInput(date: Date): string {
        return date.toISOString().split('T')[0];
    }

    openAddModal(): void {
        this.modalTitle = '➕ Agregar Nuevo Gasto';
        this.isModalOpen = true;
    }

    closeModal(): void {
        this.isModalOpen = false;
        // Limpiar formulario al cerrar
        this.newExpense = {
            description: '',
            amount: 0,
            date: new Date(),
            category: 0
        };
    }

    confirmAdd(): void {
        this.addExpense();
        this.closeModal();
    }

    openEditModal(expense: Expense): void {
        console.log('openEditModal llamado', expense);  // ← LOG
        this.editingExpense = { ...expense };
        this.isEditModalOpen = true;
        console.log('isEditModalOpen:', this.isEditModalOpen);  // ← LOG
    }

    closeEditModal(): void {
        this.isEditModalOpen = false;
        this.editingExpense = null;
    }

    confirmEdit(): void {
        this.updateExpense();
        this.closeEditModal();
    }

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