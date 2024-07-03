import { AppClient, EntryClient, ImageClient, EntryVersionClient, AuthClient } from "../api-client";

export const appClient = new AppClient();
export const entryClient = new EntryClient();
export const imageClient = new ImageClient();
export const entryVersionClient = new EntryVersionClient();
export const authClient = new AuthClient();
