import csv

csv_file = 'Sample.csv'
xml_file = 'Sample.xml'


def csv_to_xml(csv_path: str, xml_path: str):
    csv_data = csv.reader(open(csv_path, 'r'))
    xml_data = open(xml_path, 'w')

    xml_data.write('<?xml version="1.0"?>' + '\n')

    previous_row = []

    for row in csv_data:
        if previous_row == ['Order', 'Date']:
            xml_data.write('<PurchaseOrder PurchaseOrderNumber=\"{}\" OrderDate=\"{}\">\n'.format(row[0], row[1]))

        elif previous_row == ['Addresses']:
            address_row = csv_data.__next__()

            while address_row:
                xml_data.write('\t<Address Type=\"{}\">\n'.format(address_row[0]))
                xml_data.write('\t\t<Name>{}</Name>\n'.format(address_row[1]))
                xml_data.write('\t\t<Street>{}</Street>\n'.format(address_row[2]))
                xml_data.write('\t\t<City>{}</City>\n'.format(address_row[3]))
                xml_data.write('\t\t<Zip>{}</Zip>\n'.format(address_row[4]))
                xml_data.write('\t\t<Country>{}</Country>\n'.format(address_row[5]))
                xml_data.write('\t</Address>\n')

                address_row = csv_data.__next__()

        elif previous_row == ['Products']:
            product_row = csv_data.__next__()
            xml_data.write('\t<Items>\n')

            while product_row:
                xml_data.write('\t\t<Item Id=\"{}\">\n'.format(product_row[0]))
                xml_data.write('\t\t\t<ProductName>{}</ProductName>\n'.format(product_row[1]))
                xml_data.write('\t\t\t<Quantity>{}</Quantity>\n'.format(product_row[2]))
                xml_data.write('\t\t\t<PLNPrice>{}</PLNPrice>\n'.format(product_row[3]))
                xml_data.write('\t\t</Item>')
                product_row = csv_data.__next__()

            xml_data.write('\t</Items>\n')

        previous_row = row

    xml_data.write('</PurchaseOrder>')


def main():
    csv_to_xml(csv_file, xml_file)


if __name__ == '__main__':
    main()
