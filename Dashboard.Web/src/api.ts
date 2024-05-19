import { AppClient } from "../api-client/App.client";
import { AppMatcherClient } from "../api-client/AppMatcher.client";
import { EntryClient } from "../api-client/Entry.client";
import { EntryMatcherClient } from "../api-client/EntryMatcher.client";
import { ImageClient } from "../api-client/Image.client";
import { ContainerClient } from "../api-client/Container.client";

export const appClient = new AppClient();
export const appMatcherClient = new AppMatcherClient();
export const entryClient = new EntryClient();
export const entryMatcherClient = new EntryMatcherClient();
export const imageClient = new ImageClient();
export const containerClient = new ContainerClient();
