import { AppClient, EntryClient, ImageClient, PublishOrderClient, AuthClient } from "../api-client";

export const appClient = new AppClient();
export const entryClient = new EntryClient();
export const imageClient = new ImageClient();
export const publishOrderClient = new PublishOrderClient();
export const authClient = new AuthClient();
