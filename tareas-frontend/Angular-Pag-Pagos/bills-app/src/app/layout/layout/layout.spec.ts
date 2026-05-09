import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Layout } from './layout.component';
import { Header } from '../header/header';
import { Sidebar } from '../sidebar/sidebar';
import { Footer } from '../footer/footer';

@Component({
  selector: 'app-header',
  standalone: true,
  template: ''
})
class MockHeader { }

@Component({
  selector: 'app-sidebar',
  standalone: true,
  template: ''
})
class MockSidebar { }

@Component({
  selector: 'app-footer',
  standalone: true,
  template: ''
})
class MockFooter { }

describe('Layout', () => {
  let component: Layout;
  let fixture: ComponentFixture<Layout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        Layout,
        MockHeader,
        MockSidebar,
        MockFooter
      ]
    })
      .overrideComponent(Layout, {
        remove: {
          imports: [
            Header,
            Sidebar,
            Footer
          ]
        },
        add: {
          imports: [
            MockHeader,
            MockSidebar,
            MockFooter
          ]
        }
      })
      .compileComponents();

    fixture = TestBed.createComponent(Layout);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});