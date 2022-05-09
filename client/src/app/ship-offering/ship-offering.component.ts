import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { IShipOffering } from "./ship-offering";
import { ShipOfferingService } from "./ship-offering.service";

@Component({
    templateUrl: './ship-offering.component.html',
    styleUrls: ['./ship-offering.component.css']
})
export class ShipOfferingComponent implements OnInit, OnDestroy {
    errorMessage: string = '';
    sub!: Subscription;
    offerings: IShipOffering[] = [];

    constructor(private shipOfferingService: ShipOfferingService) { }

    ngOnInit(): void {
        this.sub = this.shipOfferingService.getShipOfferings().subscribe({
            next: offerings => {
                this.offerings = offerings
            },
            error: err => this.errorMessage = err
        });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}