import { Component, OnInit } from '@angular/core';

import {VehicleService} from "../../services/vehicle.service";

interface Array<T> {
    find(predicate: (search: T) => boolean): T;
}

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle.form.component.html',
    styleUrls: ['./vehicle.form.component.scss']
})
/** VehicleForm component*/
export class VehicleFormComponent implements OnInit
{
    private makes: any[];
    private models: any[];
    vehicle: any = {};
    private features: any[];

    /** VehicleForm ctor */
    constructor(private vehicleService: VehicleService) { }

    /** Called by Angular after VehicleForm component initialized */
    ngOnInit(): void {
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);

        this.vehicleService.getFeatures()
            .subscribe(features => this.features = features);
    }

    onMakeChange() {
        console.log("Vehicle", this.vehicle, this.makes, this.features);
        var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
    }
}