import { Component } from '@angular/core';
import { PersonListComponent } from "../../modules/person/person-list/person-list.component";
import { MatIcon } from '@angular/material/icon';
import { PersonMenuComponent } from '../../modules/person/person-menu/person-menu.component';
import { CommonModule } from '@angular/common';
import { PersonUpdateComponent } from "../../modules/person/person-update/person-update.component";
import { PersonCreateComponent } from "../../modules/person/person-create/person-create.component";

@Component({
  selector: 'app-person-page',
  standalone: true,
  imports: [
    CommonModule,
    MatIcon,
    PersonMenuComponent,
    PersonListComponent,
    PersonUpdateComponent,
    PersonCreateComponent
],
  templateUrl: './person-page.component.html',
  styleUrl: './person-page.component.scss'
})
export class PersonPageComponent {
  selectedComponent: string = 'personList';
  selectedPersonId?: string = '';

  onMenuSelected(component: string): void {
    this.selectedComponent = component;
  }

  onActionTriggered(event: { action: string, id?: string }): void {
    if (event.action === 'edit' && event.id) {
      this.selectedComponent = 'personUpdate';
      this.selectedPersonId = event.id;
    }
  }
}
