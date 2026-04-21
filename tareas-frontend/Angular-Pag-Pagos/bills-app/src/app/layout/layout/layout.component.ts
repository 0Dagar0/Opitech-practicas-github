import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from '../header/header';
import { Sidebar } from '../sidebar/sidebar';
import { Footer } from '../footer/footer';
@Component({
    selector: 'app-layout',
    standalone: true,
    imports: [Header, Sidebar, Footer, RouterOutlet],
    templateUrl: './layout.html',
    styleUrl: './layout.css'
})
export class Layout {
}
