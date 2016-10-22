﻿//
// This class rendering ONE sequence diagram
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ThreeDUMLAPI;

namespace View
{
    public class PackageDiagram : MonoBehaviour
    {
        #region PRIVATE VARS
        #endregion

        #region PUBLIC VARS
        public GameObject PackageGO; //Prefab
        public float packageDistance;
        public Dictionary<string, Dictionary<ThreeDUMLAPI.Package, GameObject>> Packages = new Dictionary<string, Dictionary<Package, GameObject>>();
        #endregion

        #region PUBLIC METHODS
        //This main method rendering the package diagram
        public Dictionary<string, GameObject> renderPackageDiagram(Dictionary<string, IXmlNode> packages)
        {
            Dictionary<string, GameObject> packageGOs = new Dictionary<string, GameObject>();
            float pos = 0; // Não encontrei a posição dos pacotes, então tive que criar...
            foreach (KeyValuePair<string, IXmlNode> pack in packages)
            {
                Package p = pack.Value as Package;
                //Debug.Log("Pacote " + p.Id + " -- Nome: " + p.Name + " -- Count: " + p.ClassDiagrams.Count + " - " + p.SequenceDiagrams.Count + " >> Package: " + p.IdPackage + "!!");

                GameObject packGO = (GameObject)Instantiate(PackageGO);
                packGO.transform.parent = transform;
                packGO.transform.localPosition = new Vector3(pos, 0, 0);
                packGO.name = p.Name;
                TextMesh[] texts = packGO.GetComponentsInChildren<TextMesh>();
                texts[0].text = p.Name;
                string bread = " > ";
                string parent = p.IdPackage;
                while (parent != null)
                {
                    p = (Package)packages[parent];
                    bread = " > " + p.Name + bread;
                    parent = p.IdPackage;
                }
                texts[1].text = bread;
                pos += packageDistance;
                packageGOs.Add(pack.Key, packGO);

                //Setting Packages Var
                Dictionary<ThreeDUMLAPI.Package, GameObject> Pair = new Dictionary<ThreeDUMLAPI.Package, GameObject>();
                Pair.Add((Package)pack.Value, packGO);
                Packages.Add(pack.Key, Pair);
            }
            return packageGOs;
        }
        public Dictionary<string, GameObject> renderPackageDiagram(List<string> ids, Dictionary<string, IXmlNode> packages)
        {
            Dictionary<string, GameObject> packageGOs = new Dictionary<string, GameObject>();
            float pos = 0; // Não encontrei a posição dos pacotes, então tive que criar...
            foreach (string id in ids)
            {
                Package p = packages[id] as Package;
                //Debug.Log("Pacote " + p.Id + " -- Nome: " + p.Name + " -- Count: " + p.ClassDiagrams.Count + " - " + p.SequenceDiagrams.Count + " >> Package: " + p.IdPackage + "!!");

                GameObject packGO = (GameObject)Instantiate(PackageGO);
                packGO.transform.parent = transform;
                packGO.transform.localPosition = new Vector3(pos, 0, 0);
                packGO.name = p.Name;
                TextMesh[] texts = packGO.GetComponentsInChildren<TextMesh>();
                texts[0].text = p.Name;
                string bread = " > ";
                string parent = p.IdPackage;
                while (parent != null)
                {
                    p = (Package)packages[parent];
                    bread = " > " + p.Name + bread;
                    parent = p.IdPackage;
                }
                texts[1].text = bread;
                pos += packageDistance;
                packageGOs.Add(id, packGO);
            }
            return packageGOs;
        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}