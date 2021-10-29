interface Soap {
  id: number;
  name: string;
  description: string;
  price: number;
  available: boolean;
  soapType: SoapType;
  soapTypeId: string;
  quantity: number;
  soapDetails: SoapDetail[];
  images: Image[];
}
