import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { menuItems } from '../../../config/sidebar.config';
import { MenuItem } from '../../../models/menu-item.model';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule,MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  @Input() isCollapsed: boolean = false;

  menuItems: MenuItem[] = menuItems;

  setActive(item: MenuItem) {
    // Desactivar otros elementos
    this.menuItems.forEach(m => {
      m.isActive = false;
      if (m.subMenu) {
        m.subMenu.forEach(sm => sm.isActive = false);
      }
    });

    // Activar el elemento actual
    item.isActive = true; // Para el elemento principal

    // Si el item tiene un submenú, asegúrate de que el submenú esté abierto
    if (item.subMenu) {
      item.isOpen = true; // Abre el submenú al seleccionar
    }
  }

}
