import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Expense, ExpenseCategory } from '../../core/models/expense.model';
import { ExpenseActions } from '../../core/store/expense.actions';
import {
    selectExpenses,
    selectExpensesTotal,
    selectExpensesLoading,
    selectExpensesError
} from '../../core/store/expense.reducer';
import { ModalComponent } from '../../shared/components/modal/modal.component';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-expenses',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, ModalComponent],
    templateUrl: './expenses.component.html',
    styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit, OnDestroy {
    // Observables del store
    expenses$: Observable<Expense[]>;
    total$: Observable<number>;
    loading$: Observable<boolean>;
    error$: Observable<string | null>;

    // Estados de los modales
    isModalOpen = false;
    isEditModalOpen = false;
    isDeleteModalOpen = false;
    isAddFormDirty = false;
    isEditFormDirty = false;
    showConfirmCloseModal = false;
    pendingCloseAction: 'add' | 'edit' | null = null;

    // Formularios Reactivos
    addForm: FormGroup;
    editForm: FormGroup;
    editingExpenseId: number | null = null;
    deleteId: number | null = null;

    // Títulos de modales
    modalTitle = '➕ Agregar Gasto';
    editModalTitle = '✏️ Editar Gasto';
    deleteModalTitle = '🗑️ Eliminar Gasto';

    constructor(
        private store: Store,
        private fb: FormBuilder
    ) {
        // Inicializar observables con los selectores
        this.expenses$ = this.store.select(selectExpenses);
        this.total$ = this.store.select(selectExpensesTotal);
        this.loading$ = this.store.select(selectExpensesLoading);
        this.error$ = this.store.select(selectExpensesError);

        // Crear formularios reactivos
        this.addForm = this.createEmptyForm();
        this.editForm = this.createEmptyForm();
    }

    ngOnInit(): void {
        // Cargar gastos al iniciar
        this.store.dispatch(ExpenseActions.loadExpenses());
    }

    ngOnDestroy(): void {
    }

    //  CREAR FORMULARIO VACÍO
    private createEmptyForm(): FormGroup {
        return this.fb.group({
            description: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
            amount: [null, [Validators.required, Validators.min(0.01), Validators.max(999999.99)]],
            date: [this.getTodayDateString(), Validators.required],
            category: [ExpenseCategory.Other, Validators.required]
        });
    }

    //  OBTENER FECHA ACTUAL EN FORMATO YYYY-MM-DD
    private getTodayDateString(): string {
        const today = new Date();
        return today.toISOString().split('T')[0];
    }

    //  VALIDAR FORMULARIOS
    isAddFormValid(): boolean {
        return this.addForm.valid;
    }

    isEditFormValid(): boolean {
        return this.editForm.valid;
    }

    markAddFormDirty(): void {
        this.addForm.markAsDirty();
        this.isAddFormDirty = true;
        console.log('isAddFormDirty =', this.isAddFormDirty);
    }

    markEditFormDirty(): void {
        this.editForm.markAsDirty();
        this.isEditFormDirty = true;
    }

    // 👈 RESETEAR FORMULARIOS
    resetAddForm(): void {
        this.addForm.reset({
            description: '',
            amount: null,
            date: this.getTodayDateString(),
            category: ExpenseCategory.Other
        });
        this.addForm.markAsPristine();
        this.addForm.markAsUntouched();
        this.isAddFormDirty = false;
    }

    resetEditForm(): void {
        this.editForm.reset({
            description: '',
            amount: null,
            date: this.getTodayDateString(),
            category: ExpenseCategory.Other
        });
        this.editForm.markAsPristine();
        this.editForm.markAsUntouched();
        this.editingExpenseId = null;
        this.isEditFormDirty = false;
    }

    // ABRIR MODAL DE AGREGAR
    openAddModal(): void {
        this.resetAddForm();
        this.modalTitle = '➕ Agregar Gasto';
        this.isModalOpen = true;
    }

    // ABRIR MODAL DE EDITAR
    openEditModal(expense: Expense): void {
        this.editingExpenseId = expense.id;

        // Cargar datos del gasto en el formulario
        this.editForm.patchValue({
            description: expense.description,
            amount: expense.amount,
            date: new Date(expense.date).toISOString().split('T')[0],
            category: expense.category
        });

        this.editForm.markAsPristine();
        this.editModalTitle = `✏️ Editar: ${expense.description}`;
        this.isEditModalOpen = true;
    }

    // ABRIR MODAL DE ELIMINAR
    openDeleteModal(id: number): void {
        this.deleteId = id;
        this.isDeleteModalOpen = true;
    }

    // CONFIRMAR AGREGAR
    confirmAdd(): void {
        if (this.addForm.invalid) {
            this.addForm.markAllAsTouched();
            return;
        }
        const formValue = this.addForm.value;
        const newExpense: Omit<Expense, 'id'> = {
            description: formValue.description,
            amount: formValue.amount,
            date: new Date(formValue.date),
            category: formValue.category
        };
        this.store.dispatch(ExpenseActions.addExpense({ expense: newExpense }));
        this.isAddFormDirty = false;
        this.performCloseModal();
    }

    // CONFIRMAR EDITAR
    confirmEdit(): void {
        if (this.editForm.invalid) {
            this.editForm.markAllAsTouched();
            return;
        }
        if (this.editingExpenseId === null) return;
        const formValue = this.editForm.value;
        const updatedExpense: Expense = {
            id: this.editingExpenseId,
            description: formValue.description,
            amount: formValue.amount,
            date: new Date(formValue.date),
            category: formValue.category
        };
        this.store.dispatch(ExpenseActions.updateExpense({ expense: updatedExpense }));
        this.isEditFormDirty = false;
        this.performCloseEditModal();
    }

    // CONFIRMAR ELIMINAR
    confirmDelete(): void {
        if (this.deleteId !== null) {
            this.store.dispatch(ExpenseActions.deleteExpense({ id: this.deleteId }));
            this.closeDeleteModal();
        }
    }

    // CERRAR MODALES
    closeModal(): void {
        if (this.isAddFormDirty) {
            this.pendingCloseAction = 'add';
            this.showConfirmCloseModal = true;
            return;
        }
        this.performCloseModal();
    }

    closeEditModal(): void {
        if (this.isEditFormDirty) {
            this.pendingCloseAction = 'edit';
            this.showConfirmCloseModal = true;
            return;
        }
        this.performCloseEditModal();
    }

    performCloseModal(): void {
        this.isModalOpen = false;
        this.resetAddForm();
    }

    closeDeleteModal(): void {
        this.isDeleteModalOpen = false;
        this.deleteId = null;
    }

    performCloseEditModal(): void {
        this.isEditModalOpen = false;
        this.resetEditForm();
    }

    confirmCloseModal(): void {
        if (this.pendingCloseAction === 'add') {
            this.performCloseModal();
        } else if (this.pendingCloseAction === 'edit') {
            this.performCloseEditModal();
        }
        this.showConfirmCloseModal = false;
        this.pendingCloseAction = null;
    }

    cancelCloseModal(): void {
        this.showConfirmCloseModal = false;
        this.pendingCloseAction = null;
    }

    // OBTENER NOMBRE DE CATEGORÍA
    getCategoryName(category: ExpenseCategory): string {
        const categories = {
            [ExpenseCategory.Food]: '🍔 Food',
            [ExpenseCategory.Transportation]: '🚗 Transportation',
            [ExpenseCategory.Entertainment]: '🎬 Entertainment',
            [ExpenseCategory.Utilities]: '💡 Utilities',
            [ExpenseCategory.Healthcare]: '🏥 Healthcare',
            [ExpenseCategory.Other]: '📦 Other'
        };
        return categories[category] || '📦 Other';
    }

    // GETTERS PARA ACCEDER A LOS CONTROLES EN EL HTML
    get addDescription() { return this.addForm.get('description') as FormControl; }
    get addAmount() { return this.addForm.get('amount') as FormControl; }
    get addDate() { return this.addForm.get('date') as FormControl; }
    get addCategory() { return this.addForm.get('category') as FormControl; }

    get editDescription() { return this.editForm.get('description') as FormControl; }
    get editAmount() { return this.editForm.get('amount') as FormControl; }
    get editDate() { return this.editForm.get('date') as FormControl; }
    get editCategory() { return this.editForm.get('category') as FormControl; }
}