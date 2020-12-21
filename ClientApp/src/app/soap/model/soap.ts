interface Soap {
  id: number;
  name: string;
  description: string;
  image: string;
  price: number;
  available: boolean;
  soapType: SoapType;
  soapTypeId: string;
  soapDetails: SoapDetail[];
  images: Image[];
}
