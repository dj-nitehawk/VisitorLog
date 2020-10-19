using Dom;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VisitorLog;

namespace Visits.Retrieve.ByDateRange
{
    public class Database : IDatabase
    {
        public Task<List<VisitItem>> GetVisits(string establishmentID, string fromDate, string toDate, int pageNo)
        {
            var from_date = Dates.ToUTC(fromDate).ToString("O");
            var to_date = Dates.ToUTC(toDate, "23:59").ToString("O");
            var skip_amt = pageNo > 1 ? (250 * pageNo).ToString() : "0";
            var take_amt = 250.ToString();

            var pipeline = new Template<Visit, VisitItem>(@"
            [
                {
                    $match: {
                        'Establishment.ID': ObjectId('<establishment_id>'),
                        Date: {
                            $gte: ISODate('<from_date>'),
                            $lte: ISODate('<to_date>')
                        }
                    }
                },
                {
                    $skip: <skip_amt>
                },
                {
                    $limit: <take_amt>
                },
                {
                    $project: {
                        _id: 0,
                        PersonID:
                            '$Person.ID',
                        Date: 1,
                        Remarks: 1
                    }
                },
                {
                    $lookup: {
                        from: 'Person',
                        let: { person_id: '$PersonID' },
                        pipeline: [
                            {
                                $match: {
                                    $expr: {
                                        $eq: ['$$person_id', '$_id']
                                    }
                                }
                            },
                            {
                                $project: {
                                    _id: 0,
                                    PhoneNumber: 1,
                                    IDNumber: 1,
                                    FullName: 1,
                                    Address: {
                                        $concat: [
                                            '$Address.Street', ', ',
                                            '$Address.State', ', ',
                                            '$Address.City', ', ',
                                            '$Address.ZipCode', ', ',
                                            '$Address.CountryCode']
                                    }
                                }
                            }
                        ],
                        as: 'Person'
                    }
                },
                {
                    $set: {
                        PhoneNumber: { $first: '$Person.PhoneNumber' },
                        IDNumber: { $first: '$Person.IDNumber' },
                        FullName: { $first: '$Person.FullName' },
                        Address: { $first: '$Person.Address' }
                    }
                },
                {
                    $unset:['PersonID','Person']
                }
            ]")
            .Tag("establishment_id", establishmentID)
            .Tag("from_date", from_date)
            .Tag("to_date", to_date)
            .Tag("skip_amt", skip_amt)
            .Tag("take_amt", take_amt);

            return DB.PipelineAsync(pipeline);
        }
    }

    public class VisitItem
    {
        [IgnoreDataMember] public DateTime Date { get; set; }
        public string EntryTime { get => $"{Date.ToDatePart()} {Date.ToTimePart()}"; }
        public string Remarks { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
