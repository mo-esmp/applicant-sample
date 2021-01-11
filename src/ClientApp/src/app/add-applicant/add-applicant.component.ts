import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SnotifyService } from 'ng-snotify';

import { ApplicantModel } from '../shared/models/ApplicantModel';
import { ApplicantService } from './../shared/services/applicant.service';

@Component({
  selector: 'app-add-applicant',
  templateUrl: './add-applicant.component.html',
  styleUrls: ['./add-applicant.component.scss'],
})
export class AddApplicantComponent implements OnInit {
  action: string;
  Form: FormGroup;
  dialogTitle: string;
  applicant;
  loading = false;

  constructor(
    public matDialogRef: MatDialogRef<AddApplicantComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private ApplicantService: ApplicantService,
    private _formBuilder: FormBuilder,
    private notify: SnotifyService
  ) {
    this.action = _data.action;
    if (this.action === 'edit') {
      this.dialogTitle = 'Edit Applicant';
      this.applicant = _data.applicant;
    } else {
      this.dialogTitle = 'New Applicant';
      this.applicant = {} as ApplicantModel;
    }
    this.Form = this.createForm();
  }

  ngOnInit(): void {}

  createForm(): FormGroup {
    return this._formBuilder.group({
      name: [this.applicant.name],
      familyName: [this.applicant.familyName],
      age: [this.applicant.age],
      emailAddress: [this.applicant.emailAddress],
      address: [this.applicant.address, [Validators.minLength(10)]],
      countryOfOrigin: [this.applicant.countryOfOrigin],
      hired: [this.applicant.hired],
    });
  }

  Add() {
    this.loading = true;
    this.ApplicantService.addApplicant(this.Form.value).subscribe(
      (res) => {
        this.notify.success('Applicant Added', 'Success');
        this.matDialogRef.close();
      },
      (err) => {
        this.error(err);
        this.loading = false;
      }
    );
  }

  Save() {
    this.loading = true;
    this.ApplicantService.editApplicant(
      this.applicant.id,
      this.Form.value
    ).subscribe(
      (res) => {
        this.notify.success('Applicant Added', 'Success');
        this.matDialogRef.close();
      },
      (err) => {
        this.error(err);
        this.loading = false;
      }
    );
  }

  error(err) {
    for (const errors in err.error.errors) {
      for (const error of err.error.errors[errors]) {
        this.notify.error(`${error}`, 'Error');
      }
    }
  }
}
