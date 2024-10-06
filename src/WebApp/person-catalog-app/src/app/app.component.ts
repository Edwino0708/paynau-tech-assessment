import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './shared/components/header/header.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { MainComponent } from './shared/components/main/main.component';
import { ToggleComponent } from './shared/components/toggle/toggle.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    HeaderComponent,
    SidebarComponent,
    MainComponent,
    ToggleComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  // El menú está visible por defecto
  isSidebarCollapsed = false;
  title = "PersonCatalogApp";

  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed; // Alternar el estado de colapso
  }
}
