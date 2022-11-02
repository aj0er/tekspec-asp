import { isDevMode } from "@angular/core";

export function getHttpBaseUrl(): string {
    return isDevMode() ? "https://localhost:7234" : "https://localhost:5001";
}