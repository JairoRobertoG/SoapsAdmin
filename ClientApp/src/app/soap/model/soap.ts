interface Soap {
  id: number;
  name: string;
  description: string;
  price: number;
  available: boolean;
  soapType: SoapType;
  soapTypeId: string;
  soapDetails: SoapDetail[];
  images: Image[];
}
