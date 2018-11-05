import csv
import xml.etree.ElementTree as et

xml_file = '..\TemplateGenerator\ODT\PurchaseOrderTemplate.xml'
csv_file_path = 'Sample.csv'


def write_address(address, writer):
    row = list()
    row.append(address.attrib['Type'])
    row.append(address.find('Name').text)
    row.append(address.find('Street').text)
    row.append(address.find('City').text)
    row.append(address.find('Zip').text)
    row.append(address.find('Country').text)
    writer.writerow(row)


def write_item(item, writer):
    row = list()
    row.append(item.attrib['Id'])
    row.append(item.find('ProductName').text)
    row.append(item.find('Quantity').text)
    row.append(item.find('PLNPrice').text)
    writer.writerow(row)


def xml_to_excel(file_path: str, csv_file: str):
    element_tree = et.parse(file_path)
    root = element_tree.getroot()

    with open(csv_file, 'w', newline='') as f:
        writer = csv.writer(f)

        # Writing order number and date
        writer.writerow(['Order', 'Date'])
        writer.writerow([root.attrib['PurchaseOrderNumber'], root.attrib['OrderDate']])
        writer.writerow([])

        # Getting shipping and billing addresses
        writer.writerow(['Addresses'])
        writer.writerow(['Type', 'Name', 'Street', 'City', 'Zip', 'Country'])
        for address in root.findall('Address'):
            write_address(address, writer)
        writer.writerow([])

        # Getting all ordered items
        writer.writerow(['Products'])
        writer.writerow(['Id', 'Name', 'Quantity', 'Price'])
        for item in root.find('Items').findall('Item'):
            write_item(item, writer)

        writer.writerow([])


def main():
    xml_to_excel(xml_file, csv_file_path)


if __name__ == '__main__':
    main()
