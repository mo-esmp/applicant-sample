import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

import { ApplicantModel } from './shared/models/ApplicantModel';
import { ApplicantService } from './shared/services/applicant.service';
import { AddApplicantComponent } from './add-applicant/add-applicant.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  loading = true;
  applicantList: ApplicantModel[] = [];
  ELEMENT_DATA: ApplicantModel[] = [];
  dialogRef: any;
  displayedColumns = [
    'id',
    'name',
    'familyName',
    'address',
    'countryOfOrigin',
    'emailAddress',
    'age',
    'hired',
    'operation',
  ];
  dataSource = new MatTableDataSource<ApplicantModel>(this.ELEMENT_DATA);

  constructor(
    private applicantService: ApplicantService,
    private _matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.getAllApplicants();
  }

  getAllApplicants() {
    this.clear();
    this.loading = true;
    this.applicantService.getAllApplicants().subscribe((res) => {
      this.loading = false;
      this.applicantList = res;
      this.ELEMENT_DATA = res;
      this.dataSource = new MatTableDataSource<ApplicantModel>(
        this.ELEMENT_DATA
      );
    });
  }

  clear() {
    this.applicantList = [];
    this.ELEMENT_DATA = [];
    this.dataSource = new MatTableDataSource<ApplicantModel>(this.ELEMENT_DATA);
  }

  add() {
    this.dialogRef = this._matDialog.open(AddApplicantComponent, {
      data: {
        action: 'new',
      },
    });

    this.dialogRef.afterClosed().subscribe((response) => {
      this.getAllApplicants();
    });
  }

  edit(applicant) {
    this.dialogRef = this._matDialog.open(AddApplicantComponent, {
      data: {
        applicant,
        action: 'edit',
      },
    });

    this.dialogRef.afterClosed().subscribe((response) => {
      this.getAllApplicants();
    });
  }

  delete(element) {
    this.applicantService.deleteApplicant(element.id).subscribe((res) => {
      this.getAllApplicants();
    });
  }
}
