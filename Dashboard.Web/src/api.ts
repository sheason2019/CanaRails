import { AppClient } from "../api-client/App.client";
import { EntryClient } from "../api-client/Entry.client";
import { ImageClient } from "../api-client/Image.client";
import { PublishOrderClient } from "../api-client/PublishOrder.client";

export const appClient = new AppClient();
export const entryClient = new EntryClient();
export const imageClient = new ImageClient();
export const publishOrderClient = new PublishOrderClient();
