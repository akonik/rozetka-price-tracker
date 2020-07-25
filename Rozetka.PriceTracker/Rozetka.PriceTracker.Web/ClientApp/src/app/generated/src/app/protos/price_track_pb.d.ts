// package: priceTracker
// file: src/app/protos/price_track.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";

export class TrackProductRequest extends jspb.Message {
  getProducturl(): string;
  setProducturl(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductRequest.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductRequest): TrackProductRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductRequest;
  static deserializeBinaryFromReader(message: TrackProductRequest, reader: jspb.BinaryReader): TrackProductRequest;
}

export namespace TrackProductRequest {
  export type AsObject = {
    producturl: string,
  }
}

export class TrackProcutPricesRequest extends jspb.Message {
  clearProductidsList(): void;
  getProductidsList(): Array<number>;
  setProductidsList(value: Array<number>): void;
  addProductids(value: number, index?: number): number;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProcutPricesRequest.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProcutPricesRequest): TrackProcutPricesRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProcutPricesRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProcutPricesRequest;
  static deserializeBinaryFromReader(message: TrackProcutPricesRequest, reader: jspb.BinaryReader): TrackProcutPricesRequest;
}

export namespace TrackProcutPricesRequest {
  export type AsObject = {
    productidsList: Array<number>,
  }
}

export class TrackProductPriceResponse extends jspb.Message {
  clearProductsList(): void;
  getProductsList(): Array<TrackProductResponse>;
  setProductsList(value: Array<TrackProductResponse>): void;
  addProducts(value?: TrackProductResponse, index?: number): TrackProductResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductPriceResponse.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductPriceResponse): TrackProductPriceResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductPriceResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductPriceResponse;
  static deserializeBinaryFromReader(message: TrackProductPriceResponse, reader: jspb.BinaryReader): TrackProductPriceResponse;
}

export namespace TrackProductPriceResponse {
  export type AsObject = {
    productsList: Array<TrackProductResponse.AsObject>,
  }
}

export class TrackProductResponse extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getPrice(): number;
  setPrice(value: number): void;

  getImageurl(): string;
  setImageurl(value: string): void;

  getTitle(): string;
  setTitle(value: string): void;

  getDescription(): string;
  setDescription(value: string): void;

  getUrl(): string;
  setUrl(value: string): void;

  getDiscount(): number;
  setDiscount(value: number): void;

  getStatus(): string;
  setStatus(value: string): void;

  getSellStatus(): string;
  setSellStatus(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductResponse.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductResponse): TrackProductResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductResponse;
  static deserializeBinaryFromReader(message: TrackProductResponse, reader: jspb.BinaryReader): TrackProductResponse;
}

export namespace TrackProductResponse {
  export type AsObject = {
    id: number,
    price: number,
    imageurl: string,
    title: string,
    description: string,
    url: string,
    discount: number,
    status: string,
    sellStatus: string,
  }
}

