using UnityEngine;
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBMapper.Backend.Client
{
    abstract public class AWSBaseClient : MonoBehaviour
    {
        private string IdentityPoolId = "eu-central-1:e7693afb-a19f-4273-bbe9-31adaabf8032";
        private string CognitoPoolRegion = RegionEndpoint.EUCentral1.SystemName;
        private string DynamoRegion = RegionEndpoint.EUCentral1.SystemName;

        public abstract string TableName();

        private RegionEndpoint _CognitoPoolRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoPoolRegion); }
        }

        private RegionEndpoint _DynamoRegion
        {
            get { return RegionEndpoint.GetBySystemName(DynamoRegion); }
        }

        private static IAmazonDynamoDB _ddbClient;

        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoPoolRegion);
                return _credentials;
            }
        }

        protected IAmazonDynamoDB Client
        {
            get
            {
                if (_ddbClient == null)
                {
                    _ddbClient = new AmazonDynamoDBClient(Credentials, _DynamoRegion);
                }

                return _ddbClient;
            }
        }

        protected void LoadAll<T>(Dictionary<string, Condition> filter, Action<List<T>> completion) where T : DynamoDBMapper.Model.Model, new() {
            var request = new ScanRequest
            {
                TableName = TableName()
            };
            if (filter.Count > 0) {
                request.ScanFilter = filter;
            }
            
            _ddbClient.ScanAsync(request, (result) =>
            {
                List<T> results = new List<T>();
                foreach (Dictionary<string, AttributeValue> item
                         in result.Response.Items)
                {
                    T instance = MappingExtensions.MapModel<T>(item);
                    results.Add(instance);
                }
                completion(results);
            });
        }
    }
}
