import { AppClient } from "../api-client/App.client";
import { AppMatcherClient } from "../api-client/AppMatcher.client";
import { EntryClient } from "../api-client/Entry.client";
import { ImageClient } from "../api-client/Image.client";

export const appClient = new AppClient();
export const appMatcherClient = new AppMatcherClient();
export const entryClient = new EntryClient();
export const imageClient = new ImageClient();
