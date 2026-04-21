import { Component, inject, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Expense, ExpenseCategory } from '../../core/models/expense.model';
import { ExpenseService } from '../../core/services/expense.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
    selector: 'app-expenses',
    standalone: true,
    imports: [CommonModule, CurrencyPipe, FormsModule], 
    templateUrl: './expenses.component.html',
    styleUrls: ['./expenses.component.css'],
})
export class ExpensesComponent implements OnInit {

    private expenseService = inject(ExpenseService);
    private cdr = inject(ChangeDetectorRef);

    expenses = signal<Expense[]>([]);
    totalExpenses = signal(0);

    description: string = '';
    amount: number | null = null;
    date: string = new Date().toISOString().substring(0, 10);
    category: ExpenseCategory | '' = '';
    categories: string[] = ["Comida", "Transporte", "Vivienda", "Entretenimiento", "Salud", "Otros"];

    editingExpenseId: WritableSignal<number | null> = signal(null);

    ngOnInit() {
        this.loadExpenses();
    }

    loadExpenses() {
    this.expenseService.getExpenses().subscribe((data: Expense[]) => {
        this.expenses.set(data);

        this.totalExpenses.set(
            data.reduce((acc, curr) => acc + (curr.amount || 0), 0)
        );

        this.cdr.detectChanges(); 
    });
    }

    startEdit(expense: Expense): void {
        this.editingExpenseId.set(expense.id);
        this.description = expense.description;
        this.amount = expense.amount;
        this.date = new Date(expense.date).toISOString().substring(0, 10);
        this.category = expense.category;
    }

    cancelEdit(): void {
        this.editingExpenseId.set(null);
        this.clearForm();
    }

    saveExpense(): void {
        if (!this.description || this.amount === null || this.amount <= 0 || !this.date || !this.category) {
            alert('Por favor, completa todos los campos.');
            return;
            
        }
        
        const newExpense: Omit<Expense, 'id'> = {
            description: this.description,
            amount: this.amount,
            date: new Date(this.date),
            category: this.categories.indexOf(this.category as any) as ExpenseCategory,
        };
        console.log(newExpense);

        const currentEditingId = this.editingExpenseId();

        if (currentEditingId !== null) {
            const updatedExpense: Expense = { ...newExpense, id: currentEditingId };

            this.expenseService.updateExpense(updatedExpense).subscribe({
                next: () => {
                    this.loadExpenses();
                    this.cancelEdit();
            },
            error: (err) => {
                console.error('ERROR UPDATE:', err);
                alert('Error al editar gasto');
            }
        });

        } 
        else {
            this.expenseService.addExpense(newExpense).subscribe({
                next: () => {
                    this.loadExpenses();
                    this.clearForm();
                },
                error: (err) => {
                    console.error('ERROR ADD:', err);
                    alert('Error al guardar gasto');
                }
            });
        }
    }

    deleteExpense(id: number): void {
        if (confirm('¿Estás seguro de que quieres eliminar este gasto?')) {
            this.expenseService.deleteExpense(id).subscribe(() => {
                this.loadExpenses();
            });
        }
    }

    private clearForm(): void {
        this.description = '';
        this.amount = null;
        this.date = new Date().toISOString().substring(0, 10);
        this.category = '';
    }
}