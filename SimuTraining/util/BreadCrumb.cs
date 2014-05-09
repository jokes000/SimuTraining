﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Windows;

namespace SimuTraining.util
{
    public class BreadCrumb
    {
        private BreadCrumb() { }

        private static Node root = null;
        private static XmlDocument xml = new XmlDocument();

        public static Node getRoot()
        {
            if (root == null)
            {
                xml.Load("conf/Directory.xml");
                getDirectory();
            }

            return root;
        }

        private static void getDirectory()
        {
            XmlElement element = xml.DocumentElement;
            root = new Node();

            root.Name = "首页";
            root.Parent = null;
            root.Leaf = false;
            root.Description = null;
            root.Filelocation = null;

            addChildren(root, element);
        }

        private static void addChildren(Node node, XmlNode item)
        {
            node.Name = item.Attributes["name"].Value.ToString();
            String level = item.Attributes["level"].Value.ToString();
            node.Level = Convert.ToInt32(item.Attributes["level"].Value.ToString());
            node.Leaf = item.Attributes["leaf"].Value.ToString().Equals("true");

            if (ConfigHolder.getInfo().ContainsKey(node.Name + ".d"))
            {
                String des = ConfigHolder.getInfo()[node.Name + ".d"];
                if (!des.StartsWith("        "))
                    node.Description = "        " + des;
            }
            else
            {
                node.Description = "";
            }

            if (node.Leaf && ConfigHolder.getInfo().ContainsKey(node.Name + ".f"))
            {
                node.Filelocation = "media/" + ConfigHolder.getInfo()[node.Name + ".f"];
            }
            else
            {
                node.Filelocation = "";
            }

            XmlNodeList list = item.ChildNodes;
            foreach (XmlNode child in list)
            {
                Node childNode = new Node();
                childNode.Parent = node;
                node.Children.Add(childNode);
                addChildren(childNode, child);
            }

        }

        private static String paragraph(String input)
        {
            String result = "";

            if (!input.StartsWith("        "))
                result = "        " + input;

            if (result.Length > 60)
            { 
                
            }

            return result;
        }
    }

    public class Node
    {
        private String name;
        private String description;
        private String filelocation;
        private int level;
        private Boolean leaf;
        private Node parent;
        private List<Node> children;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public String Filelocation
        {
            get { return filelocation; }
            set { filelocation = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public Boolean Leaf
        {
            get { return leaf; }
            set { leaf = value; }
        }

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public List<Node> Children
        {
            get { return children; }
            set { children = value; }
        }

        public Node() 
        {
            children = new List<Node>();
        }


    }
}