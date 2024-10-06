import { Pipe, PipeTransform } from '@angular/core';
import { GenderStatus } from '../models/genderStatus';

@Pipe({
  name: 'genderText',
  standalone: true
})
export class GenderTextPipe implements PipeTransform {
  transform(value: string): string {
    switch (value) {
      case GenderStatus.Masculino:
        return "MALE";
      case GenderStatus.Femenino:
        return "FEMALE";
      case 'OTHER':
        return 'Otro'; // Si tienes un tercer valor
      default:
        return value;
    }
  }
}
