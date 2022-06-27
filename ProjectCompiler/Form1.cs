using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCompiler
{
    
    public partial class Form1 : Form
    {
        class Symbols
        {
            public String symbol;
            public String name;
        }

        class MemorySaver
        {
            public String name;
            public String value;
        }

        class MemoryCalculating
        {
            public String name;
            public List<String> statement = new List<String>();
        }

        

        public Form1()
        {
            InitializeComponent();
        }

        List<String> iList = new List<String>();        // identifies
        List<Symbols> sList = new List<Symbols>();      // sympols
        List<String> rList = new List<String>();        // reverese words
        List<String> oList = new List<String>(); // ==> was (s)tring        // operators
        List<Label> labelsList = new List<Label>();
        List<Label> memoryLabels = new List<Label>();
        Label errLabel = new Label();
        List<MemorySaver> memoryList = new List<MemorySaver>();
        List<MemorySaver> finalMemoryList = new List<MemorySaver>();
        List<MemoryCalculating> calcList = new List<MemoryCalculating>();
        List<MemoryCalculating> tempCalcList = new List<MemoryCalculating>();

        int f = 1;
        String error = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1304, 559);
            createIdentifiers();
            createSymbols();
            createReversedWords();
        }

        public void createIdentifiers()
        {
            for (int i = 0; i < 6; i++)
            {
                String pnn="";
                switch (i)
                {
                    case 0:
                        pnn = "int";
                        break;
                    case 1:
                        pnn = "float";
                        break;
                    case 2:
                        pnn = "string";
                        break;
                    case 3:
                        pnn = "double";
                        break;
                    case 4:
                        pnn = "bool";
                        break;
                    case 5:
                        pnn = "char";
                        break;

                }
                iList.Add(pnn);
            }
        }

        public void createSymbols()
        {
            for(int i = 0; i < 17; i++)
            {
                Symbols pnn = new Symbols();
                String op = "";
                switch(i)
                {
                    case 0:
                        pnn.symbol = "+";
                        pnn.name = "operator";
                        op = "+";
                        break;
                    case 1:
                        pnn.symbol = "-";
                        pnn.name = "operator";
                        op = "-";
                        break;
                    case 2:
                        pnn.symbol = "/";
                        pnn.name = "operator";
                        op = "/";
                        break;
                    case 3:
                        pnn.symbol = "%";
                        pnn.name = "operator";
                        op = "%";
                        break;
                    case 4:
                        pnn.symbol = "*";
                        pnn.name = "operator";
                        op = "*";
                        break;
                    case 5:
                        pnn.symbol = "(";
                        pnn.name = "open bracket";
                        break;
                    case 6:
                        pnn.symbol = ")";
                        pnn.name = "close bracket";
                        break;
                    case 7:
                        pnn.symbol = "{";
                        pnn.name = "open curly bracket";
                        break;
                    case 8:
                        pnn.symbol = "}";
                        pnn.name = "close curly bracket";
                        break;
                    case 9:
                        pnn.symbol = ",";
                        pnn.name = "comma";
                        break;
                    case 10:
                        pnn.symbol = ";";
                        pnn.name = "semicolon";
                        break;
                    case 11:
                        pnn.symbol = "&&";
                        pnn.name = "and";
                        break;
                    case 12:
                        pnn.symbol = "||";
                        pnn.name = "or";
                        break;
                    case 13:
                        pnn.symbol = "<";
                        pnn.name = "less than";
                        break;
                    case 14:
                        pnn.symbol = ">";
                        pnn.name = "greater than";
                        break;
                    case 15:
                        pnn.symbol = "=";
                        pnn.name = "equal";
                        break;
                    case 16:
                        pnn.symbol = "!";
                        pnn.name = "not";
                        break;
                }
                sList.Add(pnn);
                oList.Add(op);
            }
        }

        public void createReversedWords()
        {
            for (int i = 0; i < 7; i++)
            {
                String pnn = "";
                switch (i)
                {
                    case 0:
                        pnn = "for";
                        break;
                    case 1:
                        pnn = "while";
                        break;
                    case 2:
                        pnn = "if";
                        break;
                    case 3:
                        pnn = "do";
                        break;
                    case 4:
                        pnn = "return";
                        break;
                    case 5:
                        pnn = "continue";
                        break;
                    case 6:
                        pnn = "end";
                        break;

                }
                rList.Add(pnn);
            }
        }

        public void createMemoryLabels()
        {
            this.Size = new Size(1304, 1087);       // 559 + ((memoryList.Count + calcList.Count) * 58)
            for (int j = 0; j < labelsList.Count; j++)
            {
                flowLayoutPanel1.Controls.Remove(labelsList[j]);
            }

            for (int j = 0; j < memoryLabels.Count; j++)
            {
                flowLayoutPanel1.Controls.Remove(memoryLabels[j]);
            }

            flowLayoutPanel1.Controls.Remove(errLabel);

            for (int j = 0; j < finalMemoryList.Count; j++)
            {
                Label label = new Label();
                memoryLabels.Add(label);
            }

            for (int i = 0; i < finalMemoryList.Count; i++)
            {
                memoryLabels[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                memoryLabels[i].ForeColor = System.Drawing.Color.White;
                memoryLabels[i].Name = "newLabel" + i;
                memoryLabels[i].Size = new System.Drawing.Size(1000, 36);
                memoryLabels[i].Text = finalMemoryList[i].name + " = " + finalMemoryList[i].value;
                memoryLabels[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                flowLayoutPanel1.Controls.Add(memoryLabels[i]);
            }
        }

        public void printErrors(String error, bool isCompiled)
        {
            this.Size = new Size(1304, 1087);       // 559 + ((memoryList.Count + calcList.Count) * 58)

            for (int j = 0; j < labelsList.Count; j++)
            {
                flowLayoutPanel1.Controls.Remove(labelsList[j]);
            }

            for (int j = 0; j < memoryLabels.Count; j++)
            {
                flowLayoutPanel1.Controls.Remove(memoryLabels[j]);
            }

            flowLayoutPanel1.Controls.Remove(errLabel);

            errLabel.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if(isCompiled)
                errLabel.ForeColor = System.Drawing.Color.LimeGreen;
            else
                errLabel.ForeColor = System.Drawing.Color.Red;

            errLabel.Name = "errLabel";
            errLabel.Size = new System.Drawing.Size(1050, 36);
            errLabel.Text = error;
            errLabel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
            flowLayoutPanel1.Controls.Add(errLabel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] code = textBox1.Text.Split(' ');

            for(int i = 0; i < labelsList.Count; i++)
            {
                flowLayoutPanel1.Controls.Remove(labelsList[i]);
            }

            for (int j = 0; j < memoryLabels.Count; j++)
            {
                flowLayoutPanel1.Controls.Remove(memoryLabels[j]);
            }

            flowLayoutPanel1.Controls.Remove(errLabel);

            for (int i = 0; i < code.Length; i++)
            {
                Label label = new Label();
                labelsList.Add(label);
            }

            if (!String.IsNullOrEmpty(textBox1.Text) && code[code.Length - 1] != "")
            { 
                this.Size = new Size(1304, 1087);       //559 + (code.Length * 16)
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                for (int i = 0; i < code.Length; i++)
                {
                    double test;
                    if (isIdentifier(code[i]))
                    {
                        labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelsList[i].ForeColor = System.Drawing.Color.White;
                        labelsList[i].Name = "newLabel" + i;
                        labelsList[i].Size = new System.Drawing.Size(1000, 36);
                        labelsList[i].Text = code[i] + " -> Identifier";
                        labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                        flowLayoutPanel1.Controls.Add(labelsList[i]);
                    }

                    else if (isSymbol(code[i]))
                    {
                        labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelsList[i].ForeColor = System.Drawing.Color.White;
                        labelsList[i].Name = "newLabel" + i;
                        labelsList[i].Size = new System.Drawing.Size(1000, 36);
                        labelsList[i].Text = code[i] + " -> Symbol";
                        labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                        flowLayoutPanel1.Controls.Add(labelsList[i]);
                    }

                    else if (isReversedWord(code[i]))
                    {
                        labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelsList[i].ForeColor = System.Drawing.Color.White;
                        labelsList[i].Name = "newLabel" + i;
                        labelsList[i].Size = new System.Drawing.Size(1000, 36);
                        labelsList[i].Text = code[i] + " -> Reversed Word";
                        labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                        flowLayoutPanel1.Controls.Add(labelsList[i]);
                    }
                    else if (!isIdentifier(code[i]) && !isSymbol(code[i]) && !isReversedWord(code[i]) && !code[i].All(char.IsDigit) && !Double.TryParse(code[i], out test) && (regexItem.IsMatch(code[i])))
                    {
                        labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelsList[i].ForeColor = System.Drawing.Color.White;
                        labelsList[i].Name = "newLabel" + i;
                        labelsList[i].Size = new System.Drawing.Size(1000, 36);
                        labelsList[i].Text = code[i] + " -> Variable";
                        labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                        flowLayoutPanel1.Controls.Add(labelsList[i]);
                    }
                    else if (!isIdentifier(code[i]) && !isSymbol(code[i]) && !isReversedWord(code[i]) && (code[i].All(char.IsDigit) || Double.TryParse(code[i], out test)) && !String.IsNullOrEmpty(code[i]))
                    {
                        labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelsList[i].ForeColor = System.Drawing.Color.White;
                        labelsList[i].Name = "newLabel" + i;
                        labelsList[i].Size = new System.Drawing.Size(1000, 36);
                        labelsList[i].Text = code[i] + " -> Number";
                        labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                        flowLayoutPanel1.Controls.Add(labelsList[i]);

                    }
                    else
                    {
                        if (code[i][0] != '*' && code[i][code[i].Length - 1] == '*')
                        {
                            labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            labelsList[i].ForeColor = System.Drawing.Color.White;
                            labelsList[i].Name = "newLabel" + i;
                            labelsList[i].Size = new System.Drawing.Size(1000, 36);
                            labelsList[i].Text = code[i] + " -> Pointer";
                            labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                            flowLayoutPanel1.Controls.Add(labelsList[i]);
                        }
                        else
                        {
                            labelsList[i].Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            labelsList[i].ForeColor = System.Drawing.Color.White;
                            labelsList[i].Name = "newLabel" + i;
                            labelsList[i].Size = new System.Drawing.Size(1000, 36);
                            labelsList[i].Text = code[i] + " -> Error";
                            labelsList[i].Margin = new System.Windows.Forms.Padding(6, 6, 6, 8);
                            flowLayoutPanel1.Controls.Add(labelsList[i]);
                        }
                    }
                   
                }
            }
            
            
        }

        public bool isIdentifier(String code)
        {
            for(int i = 0; i < iList.Count; i++)
            {
                if(code == iList[i])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isSymbol(String code)
        {
            for (int i = 0; i < sList.Count; i++)
            {
                if (code == sList[i].symbol)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isReversedWord(String code)
        {
            for (int i = 0; i < rList.Count; i++)
            {
                if (code == rList[i])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isVariable(String code)
        {
            double test;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (!isIdentifier(code) && !isSymbol(code) && !isReversedWord(code) && !code.All(char.IsDigit) && !Double.TryParse(code, out test) && (regexItem.IsMatch(code)))
                return true;
            else
                return false;
        }

        public bool isNumber(String code)
        {
            double test;
            if (!isIdentifier(code) && !isSymbol(code) && !isReversedWord(code) && (code.All(char.IsDigit) || Double.TryParse(code, out test)) && !String.IsNullOrEmpty(code))
           
                return true;
            else
                return false;
        }

        public bool isOperator(String code)
        {
            for (int i = 0; i < oList.Count; i++)
            {
                if (code == oList[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            this.Size = new Size(1304, 559);
        }

        public bool analyze1a(int i, string[] code, int inCondition)
        {
            MemorySaver pnn = new MemorySaver();
            if (i == 0 || inCondition == 1)
            {
                try
                {
                    if (isIdentifier(code[i]))
                    {

                        if (isVariable(code[i + 1]))
                        {
                            if (code[i + 2] == ";")
                            {
                                // well
                                pnn.name = code[i + 1];
                                pnn.value = "null";
                                memoryList.Add(pnn);
                            }
                            else
                            {
                                if (code[i + 2] == "=")
                                {
                                    if (isVariable(code[i + 3]) || isNumber(code[i + 3]))
                                    {
                                        if (code[i + 4] == ";")
                                        {
                                            // then well
                                            pnn.name = code[i + 1];
                                            pnn.value = code[i + 3];
                                            memoryList.Add(pnn);
                                        }
                                        else
                                        {
                                            for (; ; i += 2)
                                            {
                                                if (isOperator(code[i + 4]))                // || isOperator(code[i + 3][0]) && code[i + 3] == "="
                                                {
                                                    // ---->>>>> =  if the doctor wants to start with =
                                                    if (isVariable(code[i + 5]) || isNumber(code[i + 5]))
                                                    {
                                                        if (code[i + 6] == ";")
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            if (isOperator(code[i + 6]))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                f = 0;
                                                                error = "near " + code[i + 6] + " Word Number " + (i + 6);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    f = 0;
                                                    error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                                    break;
                                                }
                                            }
                                            if (f == 0) return false;
                                        }
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                        return false;
                                    }
                                }
                                else
                                {
                                    f = 0;
                                    error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i + 1] + " Word Number " + (i + 1);
                            return false;
                        }
                    }
                    else
                    {
                        f = 0;
                        error = "near " + code[i] + " Word Number " + (i);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    f = 0;
                    error = "Catched Error.";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool analyze1b(int i, string[] code, int inCondition)
        {
            MemorySaver pnn = new MemorySaver();
            if (i > 0 || inCondition == 1)
            {
                if (code[i - 1] == ";")
                {
                    try
                    {
                        if (isIdentifier(code[i]))
                        {
                            if (isVariable(code[i + 1]))
                            {
                                if (code[i + 2] == ";")
                                {
                                    pnn.name = code[i + 1];
                                    pnn.value = "null";
                                    memoryList.Add(pnn);
                                }
                                else
                                {
                                    if (code[i + 2] == "=")
                                    {
                                        if (isVariable(code[i + 3]) || isNumber(code[i + 3]))
                                        {
                                            if (code[i + 4] == ";")
                                            {
                                                // then well
                                                pnn.name = code[i + 1];
                                                pnn.value = code[i + 3];
                                                memoryList.Add(pnn);
                                            }
                                            else
                                            {
                                                for (; ; i += 2)
                                                {
                                                    if (isOperator(code[i + 4]))                // || isOperator(code[i + 3][0]) && code[i + 3] == "="
                                                    {
                                                        // ---->>>>> =  if the doctor wants to start with =
                                                        if (isVariable(code[i + 5]) || isNumber(code[i + 5]))
                                                        {
                                                            if (code[i + 6] == ";")
                                                            {
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (isOperator(code[i + 6]))
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    f = 0;
                                                                    error = "near " + code[i + 6] + " Word Number " + (i + 6);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                                        break;
                                                    }
                                                }
                                                if (f == 0) return false;
                                            }
                                        }
                                        else
                                        {
                                            f = 0;
                                            error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 1] + " Word Number " + (i + 1);
                                return false;
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i] + " Word Number " + (i);
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        f = 0;
                        error = "Catched Error.";
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool analyze2a(int i, string[] code, int inCondition)
        {
            MemoryCalculating pnn = new MemoryCalculating();
            if (i == 0 || inCondition == 1)
            {
                try
                {
                    if (isVariable(code[i]))
                    {
                        if (code[i + 1] == "=")
                        {
                            if (isVariable(code[i + 2]) || isNumber(code[i + 2]))
                            {
                                if (code[i + 3] == ";")
                                {
                                    pnn.name = code[i];
                                    pnn.statement.Add(code[i + 2]);
                                    calcList.Add(pnn);
                                    tempCalcList.Add(pnn);
                                    // then well
                                }
                                else
                                {
                                    for (int j = i; ; i += 2, j++)
                                    {
                                        if (isOperator(code[i + 3]))                // || isOperator(code[i + 3][0]) && code[i + 3] == "="
                                        {
                                            // ---->>>>> =  if the doctor wants to start with =
                                            if (isVariable(code[i + 4]) || isNumber(code[i + 4]))
                                            {
                                                if (code[i + 5] == ";")
                                                {
                                                    if (j == i)
                                                    {
                                                        pnn.name = code[j];
                                                        pnn.statement.Add(code[i + 2]);
                                                        pnn.statement.Add(code[i + 3]);
                                                        pnn.statement.Add(code[i + 4]);
                                                        calcList.Add(pnn);
                                                        tempCalcList.Add(pnn);
                                                    }
                                                    else
                                                    {
                                                        pnn.statement.Add(code[i + 3]);
                                                        pnn.statement.Add(code[i + 4]);
                                                        calcList.Add(pnn);
                                                        tempCalcList.Add(pnn);
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    if (isOperator(code[i + 5]))
                                                    {
                                                        if (j == i)
                                                        {
                                                            pnn.name = code[j];
                                                            pnn.statement.Add(code[i + 2]);
                                                            pnn.statement.Add(code[i + 3]);
                                                            pnn.statement.Add(code[i + 4]);
                                                        }
                                                        else
                                                        {
                                                            pnn.statement.Add(code[i + 3]);
                                                            pnn.statement.Add(code[i + 4]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                f = 0;
                                                error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            f = 0;
                                            error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                            break;
                                        }
                                    }
                                    if (f == 0) return false;
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                return false;
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i + 1] + " Word Number " + (i + 1);
                            return false;
                        }
                    }
                    else
                    {
                        f = 0;
                        error = "near " + code[i] + " Word Number " + (i);
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    f = 0;
                    error = "Catched Error.";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool analyze2b(int i, string[] code, int inCondition)
        {
            MemoryCalculating pnn = new MemoryCalculating();
            if (i > 0 || inCondition == 1)
            {
                if (code[i - 1] == ";")
                {
                    try
                    {
                        if (isVariable(code[i]))
                        {
                            if (code[i + 1] == "=")
                            {
                                if (isVariable(code[i + 2]) || isNumber(code[i + 2]))
                                {
                                    if (code[i + 3] == ";")
                                    {
                                        // then well
                                        pnn.name = code[i];
                                        pnn.statement.Add(code[i + 2]);
                                        calcList.Add(pnn);
                                        tempCalcList.Add(pnn);
                                    }
                                    else
                                    {
                                        for (int j = i; ; i += 2)
                                        {
                                            if (isOperator(code[i + 3]))                // || isOperator(code[i + 3][0]) && code[i + 3] == "="
                                            {
                                                // ---->>>>> =  if the doctor wants to start with =       
                                                if (isVariable(code[i + 4]) || isNumber(code[i + 4]))
                                                {
                                                    if (code[i + 5] == ";")
                                                    {
                                                        if (j == i)
                                                        {
                                                            pnn.name = code[j];
                                                            pnn.statement.Add(code[i + 2]);
                                                            pnn.statement.Add(code[i + 3]);
                                                            pnn.statement.Add(code[i + 4]);
                                                            calcList.Add(pnn);
                                                            tempCalcList.Add(pnn);
                                                        }
                                                        else
                                                        {
                                                            pnn.statement.Add(code[i + 3]);
                                                            pnn.statement.Add(code[i + 4]);
                                                            calcList.Add(pnn);
                                                            tempCalcList.Add(pnn);
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if (isOperator(code[i + 5]))
                                                        {
                                                            if (j == i)
                                                            {
                                                                pnn.name = code[j];
                                                                pnn.statement.Add(code[i + 2]);
                                                                pnn.statement.Add(code[i + 3]);
                                                                pnn.statement.Add(code[i + 4]);
                                                            }
                                                            else
                                                            {
                                                                pnn.statement.Add(code[i + 3]);
                                                                pnn.statement.Add(code[i + 4]);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    f = 0;
                                                    error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                f = 0;
                                                error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                                break;
                                            }
                                        }
                                        if (f == 0) return false;
                                    }
                                }
                                else
                                {
                                    f = 0;
                                    error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                    return false;
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 1] + " Word Number " + (i + 1);
                                return false;
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i] + " Word Number " + (i);
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        f = 0;
                        error = "Catched Error.";
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool analyze3a(int i, string[] code)
        {
            if (i == 0)
            {
                try
                {
                    if (code[i] == "if")
                    {
                        if (code[i + 1] == "(")
                        {
                            if (isVariable(code[i + 2]))
                            {
                                if (code[i + 3] == "==" || code[i + 3] == "<" || code[i + 3] == ">" || code[i + 3] == "!=" || code[i + 3] == ">=" || code[i + 3] == "<=")
                                {
                                    if (isNumber(code[i + 4]) || isVariable(code[i + 4]))
                                    {
                                        if (code[i + 5] == ")")
                                        {
                                            if (code[i + 6] == "{")
                                            {
                                                if (isIdentifier(code[i + 7]) && (analyze1a(i + 7, code, 1)))
                                                {
                                                    if(code[code.Length - 1] == "}")
                                                    {
                                                        // Then well
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                        return false;
                                                    }
                                                }
                                                else if (isIdentifier(code[i + 7]) && (analyze1b(i + 7, code, 1)))
                                                {
                                                    if (code[code.Length - 1] == "}")
                                                    {
                                                        // Then well
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                        return false;
                                                    }
                                                }
                                                else if (isVariable(code[i + 7]) && (analyze2a(i + 7, code, 1)))
                                                {
                                                    if (code[code.Length - 1] == "}")
                                                    {
                                                        // Then well
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                        return false;
                                                    }
                                                }
                                                else if (isVariable(code[i + 7]) && (analyze2b(i + 7, code, 1)))
                                                {
                                                    if (code[code.Length - 1] == "}")
                                                    {
                                                        // Then well
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    f = 0;
                                                    error = "near " + code[i + 7] + " Word Number " + (i + 7);
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                f = 0;
                                                error = "near " + code[i + 6] + " Word Number " + (i + 6);
                                                return false;
                                            }
                                        }
                                        else 
                                        {
                                            for(;; i+=4)        //
                                            {
                                                if (code[i + 5] == "&&" || code[i + 5] == "||")
                                                {
                                                    // loop
                                                    if(analyze3Loop(i + 4, code))
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    f = 0;
                                                    error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                    return false;
                                                }
                                                if (f == 0) break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                        return false;
                                    }
                                }
                                else
                                {
                                    f = 0;
                                    error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                    return false;
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                return false;
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i + 1] + " Word Number " + (i + 1);
                            return false;
                        }
                    }
                    else
                    {
                        f = 0;
                        error = "near " + code[i] + " Word Number " + (i);
                        return false;
                    }

                }
                catch
                {
                    f = 0;
                    error = "Catched Error";
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool analyze3Loop(int i, string[] code)
        {
            if (isVariable(code[i + 2]))
            {
                if (code[i + 3] == "==" || code[i + 3] == "<" || code[i + 3] == ">" || code[i + 3] == "!=" || code[i + 3] == ">=" || code[i + 3] == "<=")
                {
                    if (isNumber(code[i + 4]) || isVariable(code[i + 4]))
                    {
                        if (code[i + 5] == ")")
                        {
                            if (code[i + 6] == "{")
                            {
                                if (isIdentifier(code[i + 7]) && (analyze1a(i + 7, code, 1)))
                                {
                                    if (code[code.Length - 1] == "}")
                                    {
                                        // Then well
                                        return true;
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                        return false;
                                    }
                                }
                                else if (isIdentifier(code[i + 7]) && (analyze1b(i + 7, code, 1)))
                                {
                                    if (code[code.Length - 1] == "}")
                                    {
                                        // Then well
                                        return true;
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                        return false;
                                    }
                                }
                                else if (isVariable(code[i + 7]) && (analyze2a(i + 7, code, 1)))
                                {
                                    if (code[code.Length - 1] == "}")
                                    {
                                        // Then well
                                        return true;
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                        return false;
                                    }
                                }
                                else if (isVariable(code[i + 7]) && (analyze2b(i + 7, code, 1)))
                                {
                                    if (code[code.Length - 1] == "}")
                                    {
                                        // Then well
                                        return true;
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                        return false;
                                    }
                                }
                                else
                                {
                                    f = 0;
                                    error = "near " + code[i + 7] + " Word Number " + (i + 7);
                                    return false;
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 6] + " Word Number " + (i + 6);
                                return false;
                            }
                        }
                        else
                        {
                            if (code[i + 5] == "&&" || code[i + 5] == "||")
                            {
                                // then continue looping
                                return false;
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                return false;
                            }
                        
                        }
                    }
                    else
                    {
                        f = 0;
                        error = "near " + code[i + 4] + " Word Number " + (i + 4);
                        return false;
                    }
                }
                else
                {
                    f = 0;
                    error = "near " + code[i + 3] + " Word Number " + (i + 3);
                    return false;
                }
            }
            else
            {
                f = 0;
                error = "near " + code[i + 2] + " Word Number " + (i + 2);
                return false;
            }
            return true;
        }

        public bool analyze3b(int i, string[] code)
        {
            if (i > 0)
            {
                if (code[i - 1] == ";")
                {
                    try
                    {
                        if (code[i] == "if")
                        {
                            if (code[i + 1] == "(")
                            {
                                if (isVariable(code[i + 2]))
                                {
                                    if (code[i + 3] == "==" || code[i + 3] == "<" || code[i + 3] == ">" || code[i + 3] == "!=" || code[i + 3] == ">=" || code[i + 3] == "<=")
                                    {
                                        if (isNumber(code[i + 4]) || isVariable(code[i + 4]))
                                        {
                                            if (code[i + 5] == ")")
                                            {
                                                if (code[i + 6] == "{")
                                                {
                                                    if (isIdentifier(code[i + 7]) && (analyze1a(i + 7, code, 1)))
                                                    {
                                                        if (code[code.Length - 1] == "}")
                                                        {
                                                            // Then well
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                            return false;
                                                        }
                                                    }
                                                    else if (isIdentifier(code[i + 7]) && (analyze1b(i + 7, code, 1)))
                                                    {
                                                        if (code[code.Length - 1] == "}")
                                                        {
                                                            // Then well
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                            return false;
                                                        }
                                                    }
                                                    else if (isVariable(code[i + 7]) && (analyze2a(i + 7, code, 1)))
                                                    {
                                                        if (code[code.Length - 1] == "}")
                                                        {
                                                            // Then well
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                            return false;
                                                        }
                                                    }
                                                    else if (isVariable(code[i + 7]) && (analyze2b(i + 7, code, 1)))
                                                    {
                                                        if (code[code.Length - 1] == "}")
                                                        {
                                                            // Then well
                                                        }
                                                        else
                                                        {
                                                            f = 0;
                                                            error = "near " + code[code.Length - 1] + " Word Number " + (code.Length - 1);
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[i + 7] + " Word Number " + (i + 7);
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    f = 0;
                                                    error = "near " + code[i + 6] + " Word Number " + (i + 6);
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                for (; ; i += 4)        //
                                                {
                                                    if (code[i + 5] == "&&" || code[i + 5] == "||")
                                                    {
                                                        // loop
                                                        if (analyze3Loop(i + 4, code))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f = 0;
                                                        error = "near " + code[i + 5] + " Word Number " + (i + 5);
                                                        return false;
                                                    }
                                                    if (f == 0) break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            f = 0;
                                            error = "near " + code[i + 4] + " Word Number " + (i + 4);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        f = 0;
                                        error = "near " + code[i + 3] + " Word Number " + (i + 3);
                                        return false;
                                    }
                                }
                                else
                                {
                                    f = 0;
                                    error = "near " + code[i + 2] + " Word Number " + (i + 2);
                                    return false;
                                }
                            }
                            else
                            {
                                f = 0;
                                error = "near " + code[i + 1] + " Word Number " + (i + 1);
                                return false;
                            }
                        }
                        else
                        {
                            f = 0;
                            error = "near " + code[i] + " Word Number " + (i);
                            return false;
                        }

                    }

                    catch
                    {
                        f = 0;
                        error = "Catched Error";
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool mainAnalyze(int whichButton)
        {
            string[] code = textBox1.Text.Split(' ');
            f = 1;
            error = "";
            double test;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (code.Length >= 3)
            {

                for (int i = 0; i < code.Length; i++)
                {
                    if (isIdentifier(code[i]))
                    {
                        analyze1a(i, code, 0);
                        analyze1b(i, code, 0);
                    }

                    else if (isVariable(code[i]))
                    {
                        analyze2a(i, code, 0);
                        analyze2b(i, code, 0);
                    }

                    else if (code[i] == "if")
                    {
                        analyze3a(i, code);
                        analyze3b(i, code);

                    }

                    if (!code[i].All(char.IsLetter) || Double.TryParse(code[i], out test) || String.IsNullOrEmpty(code[i]) || !regexItem.IsMatch(code[i]))
                    {
                        if (i == 0)
                        {
                            f = 0;
                            error = "Unexpected Error ";
                            break;
                        }
                        else if (i > 0)
                        {
                            if ((code[i - 1] == ";" && code[i] != "}") || code[i - 1] == "}")
                            {
                                f = 0;
                                error = "Unexpected Error ";
                                break;
                            }
                        }
                    }


                    if (f == 0) break;
                }



            }
            else
            {
                f = 0;
                error = "Error Occurred -> Too little code to compile";
            }

            if (f == 1)
            {
                if(whichButton == 3)
                {
                    //MessageBox.Show("Compiled Successfully", "Run", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    printErrors("Compiled Successfully, No Errors. Genius!", true);
                }
                return true;
            }
            else
            {
                //MessageBox.Show("Error Occurred " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printErrors("Error Occurred " + error, false);
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainAnalyze(3);
        }

        public void updateValues(int t)
        {
            for (int j = 0; j < calcList[t].statement.Count; j++)               // Made it to change only the exact statement rather than change all 
                                                                                // statements in the first go, so no conflicts and can change the value because the bug was that
                                                                                // it was changing all the statements in the first go, so it can't go into the below "if" as the calcList Name was
                                                                                // already changed when it was changing all the statements once, so if the value changed and I need to 
                                                                                // enter this "if" to change the value I was unable to do.
            {
                for (int k = 0; k < memoryList.Count; k++)
                {
                    if (calcList[t].statement[j] == memoryList[k].name && isVariable(calcList[t].statement[j]))
                    {
                        calcList[t].statement[j] = memoryList[k].value.ToString();
                    }
                }
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] code = textBox1.Text.Split(' ');
            memoryList.Clear();
            calcList.Clear();
            finalMemoryList.Clear();
            if (mainAnalyze(4) || true)
            {
                for (int i = 0; i < memoryList.Count; i++)
                {
                    //MessageBox.Show("" + memoryList[i].name + " = " + memoryList[i].value, "Memory Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MemorySaver pnn = new MemorySaver();
                    pnn.name = memoryList[i].name;
                    pnn.value = memoryList[i].value;
                    finalMemoryList.Add(pnn);
                }

                Console.WriteLine();
                Console.WriteLine();


                
                for (int i = 0; i < tempCalcList.Count; i++)
                {
                    for(int j = 0; j < tempCalcList[i].statement.Count; j++)
                    {
                        //MessageBox.Show("" + tempCalcList[i].statement[j]);
                    }
                }


                int value = 0;
                int f2 = 0;
                for (int i = 0; i < calcList.Count; i++)
                {
                    updateValues(i);        /// <=====================
                    for (int j = 0; j < calcList[i].statement.Count; j++)
                    {
                        if (j == 1)
                        {
                            try
                            {
                                if (calcList[i].statement[j] == "+")
                                {
                                    value += Int32.Parse(calcList[i].statement[j - 1]) + Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "-")
                                {
                                    value += Int32.Parse(calcList[i].statement[j - 1]) - Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "*")
                                {
                                    value += Int32.Parse(calcList[i].statement[j - 1]) * Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "/")
                                {
                                    value += Int32.Parse(calcList[i].statement[j - 1]) / Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "%")
                                {
                                    value += Int32.Parse(calcList[i].statement[j - 1]) % Int32.Parse(calcList[i].statement[j + 1]);
                                }
                            }
                            catch(Exception ex)
                            {
                                printErrors(calcList[i].name + " Can't be Calculated because it includes one or more unidentified variable", false);///
                                f2 = 1;
                            }
                            
                        }
                        else
                        {
                            try
                            {
                                if (calcList[i].statement[j] == "+")
                                {
                                    value += Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "-")
                                {
                                    value -= Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "*")
                                {
                                    value *= Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "/")
                                {
                                    value /= Int32.Parse(calcList[i].statement[j + 1]);
                                }
                                else if (calcList[i].statement[j] == "%")
                                {
                                    value %= Int32.Parse(calcList[i].statement[j + 1]);
                                }
                            }
                            catch (Exception ex)
                            {
                                printErrors(calcList[i].name + " Can't be Calculated because" + calcList[i].statement[j + 1] + "is unidentified variable", false);///
                                f2 = 1;
                            }
                        }
                    }
                    for (int t = 0; t < memoryList.Count; t++)
                    {
                        if (memoryList[t].name == calcList[i].name)
                        {
                            memoryList[t].value = value.ToString(); // <====================
                            break;
                        }
                    }
                    
                    //MessageBox.Show("" + calcList[i].name + " = " + value, "Memory Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MemorySaver pnn = new MemorySaver();
                    pnn.name = calcList[i].name;
                    if (f2 == 1)
                        pnn.value = "Undefined";
                    else
                        pnn.value = value.ToString();


                    finalMemoryList.Add(pnn);
                    value = 0;
                }
                // if f2 == 0     <=============================
                createMemoryLabels();

                /////

                for (int i = 0; i < calcList.Count; i++)
                {
                    Console.WriteLine(calcList[i].name);
                    for (int j = 0; j < calcList[i].statement.Count; j++)
                    {
                        Console.WriteLine(calcList[i].statement[j]);
                    }
                }
            }
        }

    }
}
