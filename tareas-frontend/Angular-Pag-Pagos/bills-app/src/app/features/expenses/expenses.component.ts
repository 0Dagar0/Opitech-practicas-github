import { Component, inject, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Expense, ExpenseCategory } from '../../core/models/expense.model';
import { ExpenseService } from '../../core/services/expense.service';

@Component({
    selector: 'app-expenses',
    standalone: true,
    imports: [CommonModule, CurrencyPipe, FormsModule],
    templateUrl: './expenses.component.html',
    styleUrls: ['./expenses.component.css'],
})
export class ExpensesComponent implements OnInit {

    private expenseService = inject(ExpenseService);
    
    // LOS DATOS AHORA VIENEN DEL SERVICIO (solo lectura)
    
    expenses = this.expenseService.expenses;      //  signal del servicio
    totalExpenses = this.expenseService.total;    //  signal del servicio
    
    // DATOS DEL FORMULARIO (siguen igual)
    
    description: string = '';
    amount: number | null = null;
    date: string = new Date().toISOString().substring(0, 10);
    category: ExpenseCategory | '' = '';
    categories: string[] = ["Food", "Transportation", "Entertainment", "Utilities", "Healthcare", "Other"];
    
    editingExpenseId: WritableSignal<number | null> = signal(null);
    
    // ngOnInit: solo pedimos que cargue los datos
    ngOnInit() {
        this.expenseService.loadExpenses();
    }
    
    // startEdit: cargar datos del gasto en el formulario

    startEdit(expense: Expense): void {
        this.editingExpenseId.set(expense.id);
        this.description = expense.description;
        this.amount = expense.amount;
        this.date = new Date(expense.date).toISOString().substring(0, 10);
        this.category = expense.category;
    }
    
    // cancelEdit: limpiar el modo edición
    
    cancelEdit(): void {
        this.editingExpenseId.set(null);
        this.clearForm();
    }
    
    // saveExpense: llama al servicio (NO maneja suscripciones)
    
    saveExpense(): void {
        if (!this.description || this.amount === null || this.amount <= 0 || !this.date || !this.category) {
            alert('Por favor, completa todos los campos.');
            return;
        }
        
        const newExpense: Omit<Expense, 'id'> = {
            description: this.description,
            amount: this.amount,
            date: new Date(this.date),
            category: this.category as ExpenseCategory,
        };
        
        const currentEditingId = this.editingExpenseId();
        
        if (currentEditingId !== null) {
            const updatedExpense: Expense = { ...newExpense, id: currentEditingId };
            this.expenseService.updateExpense(updatedExpense);
            this.cancelEdit();
        } else {
            this.expenseService.addExpense(newExpense);
            this.clearForm();
        }
    }
    
    // deleteExpense: llama al servicio
    deleteExpense(id: number): void {
        if (confirm('¿Estás seguro de que quieres eliminar este gasto?')) {
            this.expenseService.deleteExpense(id);
        }
    }
    
    // clearForm: limpiar el formulario
    
    private clearForm(): void {
        this.description = '';
        this.amount = null;
        this.date = new Date().toISOString().substring(0, 10);
        this.category = '';
    }
}