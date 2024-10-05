import { Component, Output, EventEmitter } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-person-menu',
  standalone: true,
  imports: [MatButton],
  templateUrl: './person-menu.component.html',
  styleUrl: './person-menu.component.scss'
})
export class PersonMenuComponent {
  @Output() menuSelected = new EventEmitter<string>();

  selectMenu(component: string): void {
    this.menuSelected.emit(component);
  }
}
