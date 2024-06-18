import { Injectable } from '@angular/core';
import { NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {
    private readonly DELIMITER = '/';

    parse(value: string): NgbDateStruct | null {
        if (value) {
            const dateParts = value.trim().split(this.DELIMITER);
            if (dateParts.length === 3) {
                return {
                    day: parseInt(dateParts[0], 10),
                    month: parseInt(dateParts[1], 10),
                    year: parseInt(dateParts[2], 10)
                };
            }
        }
        return null;
    }

    format(date: NgbDateStruct | null): string {
        return date ?
            `${this.padNumber(date.day)}${this.DELIMITER}${this.padNumber(date.month)}${this.DELIMITER}${date.year}` : '';
    }

    private padNumber(value: number): string {
        if (this.isNumber(value)) {
            return `0${value}`.slice(-2);
        } else {
            return '';
        }
    }

    isNumber(value: any): boolean {
        return !isNaN(parseInt(`${value}`, 10));
    }
}


