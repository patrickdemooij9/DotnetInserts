import type ItemListItem from "./ItemListItem";

export default interface ItemListConfig {
    items: ItemListItem[];
    addItems: ItemListItem[];

    maxItems?: number;
    newButtonLabel: string;
}