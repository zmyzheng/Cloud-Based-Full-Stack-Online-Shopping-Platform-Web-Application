import { Item } from './Item';

export class Cart {
    count: Map<string, number>;
    content: Map<string, Item>;

    constructor() {
        this.content = new Map();
        this.count = new Map();
    }

    addToLocalCart(item: Item, num: number): void {

        if (this.count.has(item.Id)) {
            const oldNum = this.count.get(item.Id);
            this.count.set(item.Id, oldNum + num);
        } else {
            this.count.set(item.Id, num);
            this.content.set(item.Id, item);
        }
    }

    getTotalPrice(): number {
        let total = 0;

        for (const key of this.count.keys()){
            total += parseFloat(this.content.get(key).Price) * this.count.get(key);
        }
        return total;

    }

    updateLocalCart(keys: string[], items: Item[], nums: number[]): void {
        this.content.clear();
        this.count.clear();
        for (let i = 0; i < keys.length; i++){
            if (nums[i] === 0) {
                continue;
            }
            this.count.set(keys[i], nums[i]);
            this.content.set(keys[i], items[i]);
        }
    }

    getInfo(): string[] {
        const result = [];
        for (const key of this.count.keys()) {
            result.push({Id: key, Count: this.count.get(key)});
        }
        return result;
    }

}
