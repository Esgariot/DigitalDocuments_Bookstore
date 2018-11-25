from fpdf import FPDF
import datetime
import sys 

#zmienne wejsciowe
refusalArgumentation = ''

#wczytanie danych do zmiennych
date = datetime.datetime.now()

#refusalArgumentation = sys.argv[1]

arg_len = len(sys.argv)

for i in range(1,len(sys.argv)):
	refusalArgumentation += sys.argv[i]
	if(i < len(sys.argv) - 1):
		refusalArgumentation += " "

#tworzenie PDFa
pdf = FPDF()

pdf.add_page()
effectivePageWidth = pdf.w - 2*pdf.l_margin
pdf.set_font("Arial", 'B', size=14)
pdf.cell(200,20, txt="The refusal of the order execution", ln=1, align="C")
pdf.set_font("Arial", size=10)
pdf.cell(200,5, txt="Gdansk, " + str(date.day) + "." + str(date.month) + "." + str(date.year) + "      ", ln=1, align="R")
pdf.ln(10)
pdf.cell(200,15, txt="Dear Customer,", ln=1, align="L")
pdf.multi_cell(effectivePageWidth, 4, "thank you very much for making the order.", 0, 'L')
pdf.multi_cell(effectivePageWidth, 4, "Concurrently, we would like to inform you, that we can not realize your order, because " + refusalArgumentation + ".", 0, 'L')
pdf.cell(200, 4, "We apologize you for this situation.", ln=1, align="L")
pdf.ln(20)
pdf.cell(200,0.15, txt="Yours faithfully,        ", ln=1, align="R")
pdf.cell(200,10, txt="LONGINUS BOOKSTORE       ", ln=1, align="R")

pdf.output("Refuse.pdf")