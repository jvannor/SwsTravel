import { IFacility } from "./facility";

export interface IShipOffering {
    id: number;
    name: string;
    origin: IFacility;
    destination: IFacility;
}